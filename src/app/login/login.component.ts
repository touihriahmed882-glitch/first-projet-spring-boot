import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../ser/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  mail: string = '';
  password: string = '';
  message: string = '';

  constructor(private loginService: LoginService, private router: Router) { }

  login() {
    this.loginService.login(this.mail, this.password).subscribe({
      next: (res: string) => {
        this.message = res; // message reçu du backend
        if (res.includes('✅')) { // connexion réussie
          this.router.navigate(['/users']); // redirection
        }
      },
      error: (err) => {
        // le backend renvoie aussi un JSON avec { message: "..." }
        this.message = err.error?.message || 'Erreur lors de la connexion ❌';
      }
    });
  }
}
