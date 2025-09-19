import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { UserComponent } from './components/user/user.component'; // chemin correct selon ton projet
import { AuthGuard } from './guards/auth.guard';


const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' }, // page par défaut
  { path: 'login', component: LoginComponent },
  { path: 'users', component: UserComponent,canActivate: [AuthGuard] },
  { path: '**', redirectTo: '/login' } // redirection pour toute route inconnue
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
