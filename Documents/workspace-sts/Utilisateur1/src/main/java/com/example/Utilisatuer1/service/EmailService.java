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

    public boolean sendPasswordResetMail(String recipient, String token) {
        try {
            String resetUrl = "http://localhost:4200/forgot-password?token=" + token;

            SimpleMailMessage message = new SimpleMailMessage();
            message.setTo(recipient);
            message.setSubject("🔑 Réinitialisation de mot de passe");
            message.setText("Bonjour,\n\nCliquez sur ce lien pour réinitialiser votre mot de passe :\n"
                    + resetUrl +
                    "\n\n⚠️ Ce lien est valable 1 heure.");

            mailSender.send(message);
            return true;
        } catch (MailException e) {
            e.printStackTrace();
            return false;
        }
    }

}
