import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  apiUrl = 'http://localhost:8080/api/users';
  headers = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  // Ajouter un utilisateur
 // Ajouter un utilisateur
  addUser(user: any): Observable<any> {
    return this.http.post<any>(this.apiUrl,user, { headers: this.headers });
  }

 updateUser(id: number, user: any): Observable<any> {
  return this.http.put<any>(this.apiUrl + '/' + id, user, { headers: this.headers });
}



  // Supprimer un utilisateur
  deleteUser(id: number): Observable<void> {
    return this.http.delete<void>(this.apiUrl + '/' + id, { headers: this.headers });
  }

  // Lister tous les utilisateurs
  getAllUsers(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + '', { headers: this.headers });
  }

  // Récupérer un utilisateur par ID
  getUserById(id: number): Observable<any> {
    return this.http.get<any>(this.apiUrl + '/' + id, { headers: this.headers });
  }
  searchUsersFlexible(keyword: string): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + '/search/flexible?keyword=' + encodeURIComponent(keyword), { headers: this.headers });
  }
}
