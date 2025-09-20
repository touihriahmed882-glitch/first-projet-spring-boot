package com.example.Utilisatuer1.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.mail.MailException;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.stereotype.Service;

@Service
public class EmailService {

    @Autowired
    private JavaMailSender mailSender;

    public boolean sendPasswordResetMail(String toEmail) {
        try {
            // Supprimer les espaces autour de l'email
            String recipient = toEmail.trim();

            SimpleMailMessage message = new SimpleMailMessage();
            message.setTo(recipient);
            message.setSubject("🔑 Mot de passe incorrect - Réinitialisation");
            message.setText("Bonjour,\n\nVous avez tenté de vous connecter avec un mot de passe incorrect. " +
                    "Si vous avez oublié votre mot de passe, veuillez cliquer sur ce lien pour le réinitialiser : " +
                    "http://localhost:4200/reset-password\n\nMerci.");
            mailSender.send(message);
            return true;
        } catch (MailException e) {
            e.printStackTrace(); // Affiche l'erreur SMTP complète
            return false;
        }
    }
}
