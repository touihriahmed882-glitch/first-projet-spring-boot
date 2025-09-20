package com.example.Utilisatuer1.controller;

import com.example.Utilisatuer1.entity.PasswordResetToken;
import com.example.Utilisatuer1.entity.ResetPasswordRequest;
import com.example.Utilisatuer1.entity.User;
import com.example.Utilisatuer1.repository.PasswordResetTokenRepository;
import com.example.Utilisatuer1.service.EmailService;
import com.example.Utilisatuer1.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDateTime;
import java.util.Optional;
import java.util.UUID;

@RestController
@RequestMapping("/api/users")
public class PasswordController {

    @Autowired
    private UserService userService;

    @Autowired
    private PasswordResetTokenRepository tokenRepository;

    @Autowired
    private EmailService emailService;

    // 🔹 Demander la réinitialisation
    @PostMapping("/forgot-password")
    public ResponseEntity<String> forgotPassword(@RequestParam String email) {
        Optional<User> userOpt = userService.findByMail(email);
        if(userOpt.isPresent()) {
            User user = userOpt.get();
            String token = UUID.randomUUID().toString();

            PasswordResetToken resetToken = new PasswordResetToken();
            resetToken.setToken(token);
            resetToken.setUser(user);
            resetToken.setExpiryDate(LocalDateTime.now().plusHours(1)); // valable 1h
            tokenRepository.save(resetToken);

            emailService.sendPasswordResetMail(user.getMail(), token);

            return ResponseEntity.ok("Email de réinitialisation envoyé !");
        } else {
            return ResponseEntity.status(404).body("Email introuvable");
        }
    }

    // 🔹 Réinitialiser le mot de passe
    @PostMapping("/reset-password")
    public ResponseEntity<String> resetPassword(@RequestBody ResetPasswordRequest request) {
        String token = request.getToken();
        String newPassword = request.getNewPassword();

        if (token == null || newPassword == null) {
            return ResponseEntity.badRequest().body("Champs manquants !");
        }

        Optional<PasswordResetToken> tokenOpt = tokenRepository.findByToken(token);
        if (tokenOpt.isPresent() && tokenOpt.get().getExpiryDate().isAfter(LocalDateTime.now())) {
            User user = tokenOpt.get().getUser();
            user.setPassword(newPassword); // ⚠️ encoder avec BCrypt
            userService.updateUser(user.getId(), user);

            return ResponseEntity.ok("Mot de passe mis à jour !");
        } else {
            return ResponseEntity.status(400).body("Token invalide ou expiré");
        }
    }


}
