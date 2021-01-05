package com.example.serwer.repository;

import com.example.serwer.model.Accounts;
import com.example.serwer.model.ServerStats;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface ServerStatsRepository extends JpaRepository<ServerStats, Integer> {
    List<ServerStats> findByPlatform(String platform);
}
