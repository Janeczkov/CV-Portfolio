package com.example.serwer.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;
import com.example.serwer.model.Accounts;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.List;

@Repository
public interface AccountsRepository extends JpaRepository<Accounts, Long> {
    List<Accounts> findByUsername(String username);
}
