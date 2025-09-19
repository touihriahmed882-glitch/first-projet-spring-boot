import { Component, OnInit } from '@angular/core';
import { LoginService } from '../ser/login.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  mail: string = '';
  token: string = '';
  newPassword: string = '';
  message: string = '';

  constructor(
    private loginService: LoginService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    // 🔹 Récupérer le token envoyé dans l’URL
    this.route.queryParams.subscribe(params => {
      this.token = params['token'] || '';
    });
  }

  resetPassword() {
    this.loginService.resetPassword(this.token, this.newPassword).subscribe({
      next: res => {
        this.message = res;
        if (res.includes('réinitialisé')) {
          // 🔹 Redirection vers la page login après succès
          this.router.navigate(['/login']);
        }
      },
      error: err => {
        this.message = err.error?.message || 'Erreur lors de la réinitialisation ❌';
      }
    });
  }
}
