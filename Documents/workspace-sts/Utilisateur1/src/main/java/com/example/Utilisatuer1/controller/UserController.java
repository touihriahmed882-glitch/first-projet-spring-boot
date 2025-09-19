package com.example.Utilisatuer1.controller;
import com.example.Utilisatuer1.dto.LoginRequest;
import com.example.Utilisatuer1.service.UserService;
import com.example.Utilisatuer1.entity.User;
import com.example.Utilisatuer1.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;

@RestController
@RequestMapping("/api/users")
public class UserController {

    @Autowired
    private UserService userService;

    // Ajouter un utilisateur
    @PostMapping
    public ResponseEntity<?> createUser(@RequestBody User user) {
        // Vérifier si le mail existe déjà
        if (userService.existsByMail(user.getMail())) {
            return ResponseEntity
                    .badRequest()
                    .body("Ce mail existe déjà. Veuillez changer le mot de passe ou utiliser un autre mail.");
        }
        
        // Sinon, ajouter l'utilisateur via le service
        User savedUser = userService.addUser(user);
        return ResponseEntity.ok(savedUser);
    }
    @GetMapping("/search/flexible")
    public Optional<User> searchUsersFlexible(@RequestParam("keyword") String keyword) {
        return userService.searchUsersFlexible(keyword);
    }
    @PostMapping("/login")
    public ResponseEntity<Map<String, String>> login(@RequestBody LoginRequest loginRequest) {
        return userService.authenticate(loginRequest.getMail(), loginRequest.getPassword())
                .map(user -> {
                    Map<String, String> response = new HashMap<>();
                    response.put("message", "✅ Connexion réussie");
                    return ResponseEntity.ok(response);
                })
                .orElseGet(() -> {
                    Map<String, String> response = new HashMap<>();
                    response.put("message", "❌ Email ou mot de passe incorrect");
                    return ResponseEntity.status(401).body(response);
                });
    }



 




    // Modifier un utilisateur
    @PutMapping("/{id}")
    public User updateUser(@PathVariable Long id, @RequestBody User user) {
        return userService.updateUser(id, user);
    }

    // Supprimer un utilisateur
    @DeleteMapping("/{id}")
    public ResponseEntity<?> deleteUser(@PathVariable Long id) {
        userService.deleteUser(id);
        return ResponseEntity.ok().build();
    }

    // Lister tous les utilisateurs
    @GetMapping
    public List<User> getAllUsers() {
        return userService.getAllUsers();
    }

    // Récupérer un utilisateur par ID
    @GetMapping("/{id}")
    public ResponseEntity<User> getUserById(@PathVariable Long id) {
        return userService.getUserById(id)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }
}