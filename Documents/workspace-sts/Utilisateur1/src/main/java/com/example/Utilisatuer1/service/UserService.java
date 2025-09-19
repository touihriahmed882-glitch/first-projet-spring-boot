package com.example.Utilisatuer1.service;
import com.example.Utilisatuer1.entity.User;
import com.example.Utilisatuer1.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class UserService {

    @Autowired
    private UserRepository userRepository;

    // Ajouter un utilisateur
    public User addUser(User user) {
        return userRepository.save(user);
    }
    public Optional<User> authenticate(String mail, String password) {
        return userRepository.findByMailAndPassword(mail, password);
    }


    // Modifier un utilisateur
    public User updateUser(Long id, User userDetails) {
        User user = userRepository.findById(id)
                .orElseThrow(() -> new RuntimeException("User not found"));
        user.setNom(userDetails.getNom());
        user.setPrenom(userDetails.getPrenom());
        user.setRole(userDetails.getRole());
        user.setMail(userDetails.getMail());
        user.setPassword(userDetails.getPassword());
        user.setAdresse(userDetails.getAdresse());
        user.setNumTelephone(userDetails.getNumTelephone());
        return userRepository.save(user);
    }
    public Optional<User> findByMail(String mail) {
        return userRepository.findByMail(mail);
    }


    // Supprimer un utilisateur
    public void deleteUser(Long id) {
        userRepository.deleteById(id);
    }
    public boolean existsByMail(String mail) {
        return userRepository.findByMail(mail).isPresent();
    }
    public Optional<User> searchUsersFlexible(String keyword) {
        if (keyword == null || keyword.isEmpty()) {
            return Optional.empty(); // si rien n'est écrit, on retourne vide
        }

        String[] parts = keyword.trim().split(" ");

        if (parts.length >= 2) {
            // Si nom et prénom complets
            String nom = parts[0];
            String prenom = parts[1];
            Optional<User> exactMatch = userRepository.findByNomIgnoreCaseAndPrenomIgnoreCase(nom, prenom);
            if (!exactMatch.isEmpty()) {
                return exactMatch;
            }
        }

        // Sinon, recherche par début ou partie du nom/prénom
        return userRepository.findByNomIgnoreCaseContainingOrPrenomIgnoreCaseContaining(keyword, keyword);
    }



    // Récupérer tous les utilisateurs
    public List<User> getAllUsers() {
        return userRepository.findAll();
    }

    // Récupérer un utilisateur par ID
    public Optional<User> getUserById(Long id) {
        return userRepository.findById(id);
    }

	
}