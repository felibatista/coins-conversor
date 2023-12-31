import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableUsersComponent } from '../../components/table-users/table-users.component';
import { Subscription, User } from '../../lib/types';
import { UserService } from '../../services/user.service';
import { FormsModule } from '@angular/forms';
import { EditUserComponent } from '../../components/edit-user/edit-user.component';
import { SubscriptionService } from '../../services/subscriptions.service';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [CommonModule, TableUsersComponent, FormsModule, EditUserComponent],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css',
})
export class UsersComponent implements OnInit {
  users: User[] = [];
  subscriptions: Subscription[] = [];
  input: string = '';

  constructor(
    private userService: UserService,
    private subscriptionService: SubscriptionService
  ) {}

  ngOnInit(): void {
    this.userService.getUsersPage(1).then((users) => {
      if (users) {
        this.users = users;
      }
    });

    this.subscriptionService.getSubscriptions().then((subscriptions) => {
      if (subscriptions) {
        this.subscriptions = subscriptions;
      }
    });
  }

  filterUsers(): void {
    if (this.input) {
      this.userService.findUser(this.input).then((users) => {
        if (users) {
          this.users = users;
        }
      });
    } else {
      this.userService.getUsersPage(1).then((users) => {
        if (users) {
          this.users = users;
        }
      });
    }
  }
}
