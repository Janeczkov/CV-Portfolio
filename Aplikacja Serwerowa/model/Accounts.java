package com.example.serwer.model;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.data.jpa.domain.support.AuditingEntityListener;

import javax.persistence.*;

@Entity
@Table(name = "accounts")
@EntityListeners(AuditingEntityListener.class)
//@JsonIgnoreProperties(value = {"createdAt", "updatedAt"},
//        allowGetters = true)
public class Accounts {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String username;
    private String password;
    private String rank;
    private int number_of_files_uploaded;
    private int number_of_files_downloaded;
    @Column(columnDefinition = "varchar(255) default 'brak'")
    private String app_language;
    private double total_time_of_uploading;
    private double total_time_of_downloading;
    private long bytes_uploaded;
    private long bytes_downloaded;
    private double megabytes_uploaded;
    private double megabytes_downloaded;
    private double average_time_of_uploading;
    private double average_time_of_downloading;
    private double raw_average_time_of_uploading;
    private double raw_average_time_of_downloading;
    private double time_per_megabyte_upload;
    private double raw_time_per_megabyte_upload;
    private double time_per_megabyte_download;
    private double raw_time_per_megabyte_download;
    private double total_latency;
    private double average_latency;


    public int getNumber_of_files_uploaded() {
        return number_of_files_uploaded;
    }

    public void setNumber_of_files_uploaded(int number_of_files_uploaded) {
        this.number_of_files_uploaded = number_of_files_uploaded;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getRank() {
        return rank;
    }

    public void setRank(String rank) {
        this.rank = rank;
    }

    public String getApp_language() {
        return app_language;
    }

    public void setApp_language(String app_language) {
        this.app_language = app_language;
    }


    public double getAverage_time_of_uploading() {
        return average_time_of_uploading;
    }

    public void setAverage_time_of_uploading(double average_time_of_uploading) {
        this.average_time_of_uploading = average_time_of_uploading;
    }

    public long getBytes_uploaded() {
        return bytes_uploaded;
    }

    public void setBytes_uploaded(long bytes_uploaded) {
        this.bytes_uploaded = bytes_uploaded;
    }

    public double getMegabytes_uploaded() {
        return megabytes_uploaded;
    }

    public void setMegabytes_uploaded(double megabytes_uploaded) {
        this.megabytes_uploaded = megabytes_uploaded;
    }

    public double getTime_per_megabyte_upload() {
        return time_per_megabyte_upload;
    }

    public void setTime_per_megabyte_upload(double time_per_megabyte_upload) {
        this.time_per_megabyte_upload = time_per_megabyte_upload;
    }

    public double getTotal_latency() {
        return total_latency;
    }

    public void setTotal_latency(double total_latency) {
        this.total_latency = total_latency;
    }

    public double getAverage_latency() {
        return average_latency;
    }

    public void setAverage_latency(double average_latency) {
        this.average_latency = average_latency;
    }

    public double getRaw_average_time_of_uploading() {
        return raw_average_time_of_uploading;
    }

    public void setRaw_average_time_of_uploading(double raw_average_time_of_uploading) {
        this.raw_average_time_of_uploading = raw_average_time_of_uploading;
    }

    public double getRaw_time_per_megabyte_upload() {
        return raw_time_per_megabyte_upload;
    }

    public void setRaw_time_per_megabyte_upload(double raw_time_per_megabyte_upload) {
        this.raw_time_per_megabyte_upload = raw_time_per_megabyte_upload;
    }

    public long getBytes_downloaded() {
        return bytes_downloaded;
    }

    public void setBytes_downloaded(long bytes_downloaded) {
        this.bytes_downloaded = bytes_downloaded;
    }

    public double getMegabytes_downloaded() {
        return megabytes_downloaded;
    }

    public void setMegabytes_downloaded(double megabytes_downloaded) {
        this.megabytes_downloaded = megabytes_downloaded;
    }

    public double getAverage_time_of_downloading() {
        return average_time_of_downloading;
    }

    public void setAverage_time_of_downloading(double average_time_of_downloading) {
        this.average_time_of_downloading = average_time_of_downloading;
    }

    public double getRaw_average_time_of_downloading() {
        return raw_average_time_of_downloading;
    }

    public void setRaw_average_time_of_downloading(double raw_average_time_of_downloading) {
        this.raw_average_time_of_downloading = raw_average_time_of_downloading;
    }

    public double getTime_per_megabyte_download() {
        return time_per_megabyte_download;
    }

    public void setTime_per_megabyte_download(double time_per_megabyte_download) {
        this.time_per_megabyte_download = time_per_megabyte_download;
    }

    public double getRaw_time_per_megabyte_download() {
        return raw_time_per_megabyte_download;
    }

    public void setRaw_time_per_megabyte_download(double raw_time_per_megabyte_download) {
        this.raw_time_per_megabyte_download = raw_time_per_megabyte_download;
    }

    public int getNumber_of_files_downloaded() {
        return number_of_files_downloaded;
    }

    public void setNumber_of_files_downloaded(int number_of_files_downloaded) {
        this.number_of_files_downloaded = number_of_files_downloaded;
    }

    public double getTotal_time_of_uploading() {
        return total_time_of_uploading;
    }

    public void setTotal_time_of_uploading(double total_time_of_uploading) {
        this.total_time_of_uploading = total_time_of_uploading;
    }

    public double getTotal_time_of_downloading() {
        return total_time_of_downloading;
    }

    public void setTotal_time_of_downloading(double total_time_of_downloading) {
        this.total_time_of_downloading = total_time_of_downloading;
    }

    /*@Column(nullable = false, updatable = false)
    @Temporal(TemporalType.TIMESTAMP)
    @CreatedDate
    private Date createdAt;

    @Column(nullable = false)
    @Temporal(TemporalType.TIMESTAMP)
    @LastModifiedDate
    private Date updatedAt;*/

}