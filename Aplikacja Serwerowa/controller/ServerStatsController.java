package com.example.serwer.controller;


import com.example.serwer.exception.ResourceNotFoundException;
import com.example.serwer.model.Accounts;
import com.example.serwer.model.Files;
import com.example.serwer.model.ServerStats;
import com.example.serwer.repository.AccountsRepository;
import com.example.serwer.repository.FilesRepository;
import com.example.serwer.repository.ServerStatsRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.annotation.PostConstruct;
import javax.validation.Valid;
import java.util.List;

@RestController
@RequestMapping("/server")
public class ServerStatsController {

    @Autowired
    private ServerStatsRepository serverStatsRepository;
    @Autowired
    private FilesRepository filesRepository;
    @Autowired
    private AccountsRepository accountsRepository;

    @GetMapping("/stats")
    public List<ServerStats> getStats() {
        /*List<Files> files = filesRepository.findAll();
        List<Accounts> users = accountsRepository.findAll();
        int size = users.size();
        for (int i = 0; i<size; i++){

        }
        System.out.println(i);*/
        return serverStatsRepository.findAll();
    }

    @PostConstruct
    public void init() {
        if (serverStatsRepository.findByPlatform("java").isEmpty()) {
            ServerStats newone = new ServerStats("java");
            serverStatsRepository.save(newone);
        }
        if (serverStatsRepository.findByPlatform("csharp").isEmpty()) {
            ServerStats newone = new ServerStats("csharp");
            serverStatsRepository.save(newone);
        }
    }

}