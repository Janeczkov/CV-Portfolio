package com.example.serwer.repository;

import com.example.serwer.model.Accounts;
import com.example.serwer.model.Files;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface FilesRepository extends JpaRepository<Files, Integer> {
    List<Files> findByFileName(String FileName);
}
