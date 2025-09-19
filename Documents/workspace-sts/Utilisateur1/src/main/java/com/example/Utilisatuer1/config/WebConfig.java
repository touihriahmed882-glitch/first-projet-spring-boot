package com.example.Utilisatuer1.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.config.annotation.CorsRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurer;

@Configuration
public class WebConfig {

    @Bean
    public WebMvcConfigurer corsConfigurer() {
        return new WebMvcConfigurer() {  // <-- Héritage de WebMvcConfigurer
            @Override
            public void addCorsMappings(CorsRegistry registry) {
                registry.addMapping("/**")   // toutes les routes
                        .allowedOrigins("http://localhost:4200") // front Angular
                        .allowedMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                        .allowedHeaders("*");
            }
        };
    }
}
