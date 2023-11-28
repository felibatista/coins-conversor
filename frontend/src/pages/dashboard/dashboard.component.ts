import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ConversionListComponent } from '../../components/conversion-list/conversion-list.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { ForeingService } from '../../services/foreing/foreing.service';
import { UserService } from '../../services/user/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  standalone: true,
  imports: [
    CommonModule,
    ConversionListComponent,
    NavbarComponent,
    FooterComponent,
  ],
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  totalConversions: number | null = null;
  conversionsThisMonth: number | null = null;
  conversionsLastMonth: number | null = null;
  conversionLimit: number | null = null;

  firstForeing: string | null = null;
  secondForeing: string | null = null;

  constructor(
    private userService: UserService,
    private foreingService: ForeingService
  ) {}

  ngOnInit(): void {
    this.userService.getConversions().then((conversions) => {
      if (conversions) {
        //timeout load
        setTimeout(() => {
          this.userService.getPlan().then((plan) => {
            if (plan) {
              this.conversionLimit = plan.limit;
            }
          });

          if (conversions.length == 0) {
            this.totalConversions = 0;
            this.conversionsThisMonth = 0;
            this.conversionsLastMonth = 0;
            this.firstForeing = '-';
            this.secondForeing = '-';

            return;
          }
          
          this.totalConversions = conversions.length;


          this.conversionsThisMonth = conversions.filter((conversion) => {
            return conversion.date.getMonth() == new Date().getMonth();
          }).length;

          this.conversionsLastMonth = conversions.filter((conversion) => {
            return conversion.date.getMonth() == new Date().getMonth() - 1;
          }).length;

          //get most used foreings
          const foreings = conversions.map((conversion) => {
            return conversion.fromForeingId;
          });

          const firstForeingId = foreings
            .sort(
              (a, b) =>
                foreings.filter((v) => v === a).length -
                foreings.filter((v) => v === b).length
            )
            .pop();

          if (!firstForeingId) {
            this.firstForeing = '-';
            this.secondForeing = '-';

            return;
          }

          foreings.splice(foreings.indexOf(firstForeingId));

          const secondForeingId = foreings
            .sort(
              (a, b) =>
                foreings.filter((v) => v === a).length -
                foreings.filter((v) => v === b).length
            )
            .pop();

          if (firstForeingId) {
            this.foreingService.getForeing(firstForeingId).then((foreing) => {
              if (foreing) {
                this.firstForeing = foreing.code;
              }
            });
          }

          if (secondForeingId) {
            if (secondForeingId == firstForeingId) {
              this.secondForeing = '-';

              return;
            }

            this.foreingService.getForeing(secondForeingId).then((foreing) => {
              if (foreing) {
                this.secondForeing = foreing.code;
              }
            });
          } else {
            this.secondForeing = '-';
          }
        }, 1000);
      }
    });
  }
}