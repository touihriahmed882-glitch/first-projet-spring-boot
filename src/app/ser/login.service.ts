import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  apiUrl = 'http://localhost:8080/api/users';
   private loggedIn = false; // état de connexion
  headers = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  login(mail: string, password: string): Observable<string> {
    const body = { mail, password };
        this.loggedIn = true;
    return this.http.post<{ message: string }>(this.apiUrl + '/login', body, { headers: this.headers })
      .pipe(
        map(res => res.message) // On récupère uniquement le message du JSON
      );
          localStorage.setItem('token', 'connected'); // ou un vrai token JWT

  }
  logout() {
    this.loggedIn = false;
    localStorage.removeItem('token'); // si tu utilises un token
  }

  isLoggedIn(): boolean {
    return this.loggedIn || !!localStorage.getItem('token');
  }
  



}
