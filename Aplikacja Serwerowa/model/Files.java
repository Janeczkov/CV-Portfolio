package com.example.serwer.model;


import javax.persistence.*;

@Entity
@Table(name = "uploads")
public class Files {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    @Column(name="filename")
    private String fileName;
    private String author;
    private String file_type;
    private long file_sizeB;
    private float file_sizeMB;
    private int number_of_downloads_java;
    private int number_of_downloads_csharp;
    private double total_time_downloaded_java;
    private double total_time_downloaded_csharp;
    private double average_time_downloaded_java;
    private double average_time_downloaded_csharp;
    private double raw_average_time_downloaded_java;
    private double raw_average_time_downloaded_csharp;
    private double time_per_megabyte_download_java;
    private double time_per_megabyte_download_csharp;
    private double raw_time_per_megabyte_download_java;
    private double raw_time_per_megabyte_download_csharp;
    private double total_latency_java;
    private double total_latency_csharp;
    private double average_latency_java;
    private double average_latency_csharp;
    //private double total

    @Transient
    private String temp_platform;
    @Transient
    private double temp_download_time;
    @Transient
    private double temp_upload_time;
    @Transient
    private double temp_avg_latency;
    @Transient
    private String temp_filename;
    @Transient
    private String temp_username;


    public Files() {

    }
    public Files(String fileName, String file_type, long file_sizeB, String author) {
        this.fileName = fileName;
        this.file_type = file_type;
        this.file_sizeB = file_sizeB;
        this.author = author;
    }


    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getFileName() {
        return fileName;
    }

    public void setFileName(String fileName) {
        this.fileName = fileName;
    }

    public String getAuthor() {
        return author;
    }

    public void setAuthor(String author) {
        this.author = author;
    }

    public String getFile_type() {
        return file_type;
    }

    public void setFile_type(String file_type) {
        this.file_type = file_type;
    }

    public long getFile_sizeB() {
        return file_sizeB;
    }

    public void setFile_sizeB(long file_sizeB) {
        this.file_sizeB = file_sizeB;
    }

    public float getFile_sizeMB() {
        return file_sizeMB;
    }

    public void setFile_sizeMB(float file_sizeMB) {
        this.file_sizeMB = file_sizeMB;
    }

    public int getNumber_of_downloads_java() {
        return number_of_downloads_java;
    }

    public void setNumber_of_downloads_java(int number_of_downloads_java) {
        this.number_of_downloads_java = number_of_downloads_java;
    }

    public int getNumber_of_downloads_csharp() {
        return number_of_downloads_csharp;
    }

    public void setNumber_of_downloads_csharp(int number_of_downloads_csharp) {
        this.number_of_downloads_csharp = number_of_downloads_csharp;
    }

    public double getTotal_time_downloaded_java() {
        return total_time_downloaded_java;
    }

    public void setTotal_time_downloaded_java(double total_time_downloaded_java) {
        this.total_time_downloaded_java = total_time_downloaded_java;
    }

    public double getTotal_time_downloaded_csharp() {
        return total_time_downloaded_csharp;
    }

    public void setTotal_time_downloaded_csharp(double total_time_downloaded_csharp) {
        this.total_time_downloaded_csharp = total_time_downloaded_csharp;
    }

    public double getAverage_time_downloaded_java() {
        return average_time_downloaded_java;
    }

    public void setAverage_time_downloaded_java(double average_time_downloaded_java) {
        this.average_time_downloaded_java = average_time_downloaded_java;
    }

    public double getAverage_time_downloaded_csharp() {
        return average_time_downloaded_csharp;
    }

    public void setAverage_time_downloaded_csharp(double average_time_downloaded_csharp) {
        this.average_time_downloaded_csharp = average_time_downloaded_csharp;
    }

    public String getTemp_platform() {
        return temp_platform;
    }

    public void setTemp_platform(String temp_platform) {
        this.temp_platform = temp_platform;
    }

    public double getTemp_download_time() {
        return temp_download_time;
    }

    public void setTemp_download_time(double temp_download_time) {
        this.temp_download_time = temp_download_time;
    }

    public double getTemp_upload_time() {
        return temp_upload_time;
    }

    public void setTemp_upload_time(double temp_upload_time) {
        this.temp_upload_time = temp_upload_time;
    }

    public double getTemp_avg_latency() {
        return temp_avg_latency;
    }

    public void setTemp_avg_latency(double temp_avg_latency) {
        this.temp_avg_latency = temp_avg_latency;
    }

    public double getTime_per_megabyte_download_java() {
        return time_per_megabyte_download_java;
    }

    public void setTime_per_megabyte_download_java(double time_per_megabyte_download_java) {
        this.time_per_megabyte_download_java = time_per_megabyte_download_java;
    }

    public double getTime_per_megabyte_download_csharp() {
        return time_per_megabyte_download_csharp;
    }

    public void setTime_per_megabyte_download_csharp(double time_per_megabyte_download_csharp) {
        this.time_per_megabyte_download_csharp = time_per_megabyte_download_csharp;
    }

    public double getTotal_latency_java() {
        return total_latency_java;
    }

    public void setTotal_latency_java(double total_latency_java) {
        this.total_latency_java = total_latency_java;
    }

    public double getTotal_latency_csharp() {
        return total_latency_csharp;
    }

    public void setTotal_latency_csharp(double total_latency_csharp) {
        this.total_latency_csharp = total_latency_csharp;
    }

    public double getAverage_latency_java() {
        return average_latency_java;
    }

    public void setAverage_latency_java(double average_latency_java) {
        this.average_latency_java = average_latency_java;
    }

    public double getAverage_latency_csharp() {
        return average_latency_csharp;
    }

    public void setAverage_latency_csharp(double average_latency_csharp) {
        this.average_latency_csharp = average_latency_csharp;
    }

    public double getRaw_average_time_downloaded_java() {
        return raw_average_time_downloaded_java;
    }

    public void setRaw_average_time_downloaded_java(double raw_average_time_downloaded_java) {
        this.raw_average_time_downloaded_java = raw_average_time_downloaded_java;
    }

    public double getRaw_average_time_downloaded_csharp() {
        return raw_average_time_downloaded_csharp;
    }

    public void setRaw_average_time_downloaded_csharp(double raw_average_time_downloaded_csharp) {
        this.raw_average_time_downloaded_csharp = raw_average_time_downloaded_csharp;
    }

    public double getRaw_time_per_megabyte_download_java() {
        return raw_time_per_megabyte_download_java;
    }

    public void setRaw_time_per_megabyte_download_java(double raw_time_per_megabyte_download_java) {
        this.raw_time_per_megabyte_download_java = raw_time_per_megabyte_download_java;
    }

    public double getRaw_time_per_megabyte_download_csharp() {
        return raw_time_per_megabyte_download_csharp;
    }

    public void setRaw_time_per_megabyte_download_csharp(double raw_time_per_megabyte_download_csharp) {
        this.raw_time_per_megabyte_download_csharp = raw_time_per_megabyte_download_csharp;
    }

    public String getTemp_filename() {
        return temp_filename;
    }

    public void setTemp_filename(String temp_filename) {
        this.temp_filename = temp_filename;
    }

    public String getTemp_username() {
        return temp_username;
    }

    public void setTemp_username(String temp_username) {
        this.temp_username = temp_username;
    }
}