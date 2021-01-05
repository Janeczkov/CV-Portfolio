package com.example.serwer;

import com.example.serwer.property.FileStorageProperties;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.data.jpa.repository.config.EnableJpaAuditing;

@SpringBootApplication
@EnableJpaAuditing
@EnableConfigurationProperties({
		FileStorageProperties.class
})
public class SerwerApplication {

	public static void main(String[] args) {
		SpringApplication.run(SerwerApplication.class, args);
	}
}