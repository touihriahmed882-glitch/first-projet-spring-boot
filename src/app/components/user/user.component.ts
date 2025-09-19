import { Component, OnInit } from '@angular/core';
import { UserService } from '../../ser/user.service';
import { LoginService } from '../../ser/login.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  users: any[] = [];
  selectedUser: any = {}; // utilisateur sélectionné pour modification
newUser: any = {
  nom: '',
  prenom: '',
  role: '',
  mail: '',
  password: '',
  adresse: '',
  numTelephone: ''
};  message: string = '';
  searchKeyword: string | undefined;

  constructor(private userService: UserService,
        private loginService: LoginService,
    private router: Router

  ) { }

  ngOnInit(): void {
    this.loadUsers();
  }

 loadUsers(): void {
  this.userService.getAllUsers().subscribe(
    (data) => {
      this.users = Array.isArray(data) ? data : [data];
    },
    (error) => {
      console.error('Erreur lors du chargement des utilisateurs', error);
    }
  );
}

 searchUsers(): void {
  if (!this.searchKeyword || this.searchKeyword.trim() === '') {
    this.loadUsers();
    return;
  }

  this.userService.searchUsersFlexible(this.searchKeyword).subscribe(
    (data) => {
      // Si data est un objet unique -> on le met dans un tableau
      this.users = Array.isArray(data) ? data : [data];
    },
    (error) => {
      console.error(error);
      this.message = 'Erreur lors de la recherche ❌';
    }
  );
}


  // Sélectionner un utilisateur pour modification
  selectUser(user: any): void {
    this.selectedUser = { ...user }; // copier les données pour éviter la modification directe
    this.message = '';
  }

  // Ajouter un nouvel utilisateur
  addUser(): void {
    this.userService.addUser(this.newUser).subscribe(
      (data) => {
        this.message = 'Utilisateur ajouté avec succès ✅';
        this.users.push(data); // ajouter à la liste locale
        this.newUser = {}; // réinitialiser le formulaire
      },
      (error) => {
        console.error(error);
        this.message = 'Erreur lors de l\'ajout de l\'utilisateur ❌';
      }
    );
  }

  // Mettre à jour l'utilisateur sélectionné
  updateUser(): void {
    if (!this.selectedUser.id) {
      this.message = 'Aucun utilisateur sélectionné pour la mise à jour ❌';
      return;
    }

    this.userService.updateUser(this.selectedUser.id, this.selectedUser).subscribe(
      (data) => {
        this.message = 'Utilisateur modifié avec succès ✅';
        this.loadUsers(); // recharger les utilisateurs pour voir les changements
        this.selectedUser = {}; // réinitialiser la sélection
      },
      (error) => {
        console.error(error);
        this.message = 'Erreur lors de la modification de l\'utilisateur ❌';
      }
    );
  }

  // Supprimer un utilisateur
  deleteUser(id: number): void {
    if (!confirm('Voulez-vous vraiment supprimer cet utilisateur ?')) return;

    this.userService.deleteUser(id).subscribe(
      () => {
        this.message = 'Utilisateur supprimé avec succès ✅';
        this.users = this.users.filter(u => u.id !== id); // retirer de la liste locale
      },
      (error) => {
        console.error(error);
        this.message = 'Erreur lors de la suppression de l\'utilisateur ❌';
      }
    );
  }

  // Annuler la modification
  cancelEdit(): void {
    this.selectedUser = {};
    this.message = '';
  }
  logout() {
    this.loginService.logout();
    this.router.navigate(['/login']); // redirection vers login
  }
}