package com.example.Utilisatuer1.repository;

import com.example.Utilisatuer1.entity.User; // l'import doit correspondre à ton package entity

import java.util.Optional;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface UserRepository extends JpaRepository<User, Long> {

	Optional<User> findById(Long id);
	Optional<User> findByMail(String mail);
	 // Recherche flexible : nom ou prénom contenant une chaîne
	Optional<User> findByNomIgnoreCaseContainingOrPrenomIgnoreCaseContaining(String nom, String prenom);

    // Recherche exacte nom et prénom
	Optional<User> findByNomIgnoreCaseAndPrenomIgnoreCase(String nom, String prenom);


    // JpaRepository fournit déjà toutes les opérations CRUD de base :
    // save(), findById(), findAll(), deleteById(), etc.
}