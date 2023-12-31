import { inject } from '@angular/core';
import type { CanActivateFn } from '@angular/router';
import { LoginService } from '../services/login.service';

export const notLoggedGuard: CanActivateFn = (route, state) => {
  const loginService = inject(LoginService);

  if (!loginService.isLogged()) {
    return true;
  }

  window.document.location.href = '/';
  return false;
};
