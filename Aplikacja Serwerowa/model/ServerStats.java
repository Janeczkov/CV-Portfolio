
package com.example.serwer.model;


import javax.annotation.PostConstruct;
import javax.persistence.*;

@Entity
@Table(name = "server_stats")
public class ServerStats {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    private String platform;
    private int total_files_uploaded;
    private int total_files_downloaded;
    private long total_B_size_of_files_uploaded;
    private double total_MB_size_of_files_uploaded;
    private long total_B_size_of_files_downloaded;
    private double total_MB_size_of_files_downloaded;
    private double total_time_uploaded;
    private double total_time_downloaded;
    private double average_time_uploaded;
    private double average_time_downloaded;
    private double raw_average_time_uploaded;
    private double raw_average_time_downloaded;
    private double time_per_megabyte_upload;
    private double time_per_megabyte_download;
    private double raw_time_per_megabyte_upload;
    private double raw_time_per_megabyte_download;
    private double total_latency_upload;
    private double average_latency_upload;
    private double total_latency_download;
    private double average_latency_download;


    @Transient
    private String temp_platform;
    @Transient
    private double temp_download_time;
    @Transient
    private double temp_upload_time;
    @Transient
    private double temp_avg_latency;



    public ServerStats() {

    }

    public ServerStats(String platform) {
        this.platform = platform;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getPlatform() {
        return platform;
    }

    public void setPlatform(String platform) {
        this.platform = platform;
    }

    public long getTotal_B_size_of_files_uploaded() {
        return total_B_size_of_files_uploaded;
    }

    public void setTotal_B_size_of_files_uploaded(long total_B_size_of_files_uploaded) {
        this.total_B_size_of_files_uploaded = total_B_size_of_files_uploaded;
    }

    public double getTotal_MB_size_of_files_uploaded() {
        return total_MB_size_of_files_uploaded;
    }

    public void setTotal_MB_size_of_files_uploaded(double total_MB_size_of_files_uploaded) {
        this.total_MB_size_of_files_uploaded = total_MB_size_of_files_uploaded;
    }

    public long getTotal_B_size_of_files_downloaded() {
        return total_B_size_of_files_downloaded;
    }

    public void setTotal_B_size_of_files_downloaded(long total_B_size_of_files_downloaded) {
        this.total_B_size_of_files_downloaded = total_B_size_of_files_downloaded;
    }

    public double getTotal_MB_size_of_files_downloaded() {
        return total_MB_size_of_files_downloaded;
    }

    public void setTotal_MB_size_of_files_downloaded(double total_MB_size_of_files_downloaded) {
        this.total_MB_size_of_files_downloaded = total_MB_size_of_files_downloaded;
    }

    public double getTotal_time_uploaded() {
        return total_time_uploaded;
    }

    public void setTotal_time_uploaded(double total_time_uploaded) {
        this.total_time_uploaded = total_time_uploaded;
    }

    public double getTotal_time_downloaded() {
        return total_time_downloaded;
    }

    public void setTotal_time_downloaded(double total_time_downloaded) {
        this.total_time_downloaded = total_time_downloaded;
    }

    public double getAverage_time_uploaded() {
        return average_time_uploaded;
    }

    public void setAverage_time_uploaded(double average_time_uploaded) {
        this.average_time_uploaded = average_time_uploaded;
    }

    public double getAverage_time_downloaded() {
        return average_time_downloaded;
    }

    public void setAverage_time_downloaded(double average_time_downloaded) {
        this.average_time_downloaded = average_time_downloaded;
    }

    public double getRaw_average_time_uploaded() {
        return raw_average_time_uploaded;
    }

    public void setRaw_average_time_uploaded(double raw_average_time_uploaded) {
        this.raw_average_time_uploaded = raw_average_time_uploaded;
    }

    public double getRaw_average_time_downloaded() {
        return raw_average_time_downloaded;
    }

    public void setRaw_average_time_downloaded(double raw_average_time_downloaded) {
        this.raw_average_time_downloaded = raw_average_time_downloaded;
    }

    public double getTime_per_megabyte_upload() {
        return time_per_megabyte_upload;
    }

    public void setTime_per_megabyte_upload(double time_per_megabyte_upload) {
        this.time_per_megabyte_upload = time_per_megabyte_upload;
    }

    public double getTime_per_megabyte_download() {
        return time_per_megabyte_download;
    }

    public void setTime_per_megabyte_download(double time_per_megabyte_download) {
        this.time_per_megabyte_download = time_per_megabyte_download;
    }

    public double getRaw_time_per_megabyte_upload() {
        return raw_time_per_megabyte_upload;
    }

    public void setRaw_time_per_megabyte_upload(double raw_time_per_megabyte_upload) {
        this.raw_time_per_megabyte_upload = raw_time_per_megabyte_upload;
    }

    public double getRaw_time_per_megabyte_download() {
        return raw_time_per_megabyte_download;
    }

    public void setRaw_time_per_megabyte_download(double raw_time_per_megabyte_download) {
        this.raw_time_per_megabyte_download = raw_time_per_megabyte_download;
    }

    public double getTotal_latency_upload() {
        return total_latency_upload;
    }

    public void setTotal_latency_upload(double total_latency_upload) {
        this.total_latency_upload = total_latency_upload;
    }

    public double getAverage_latency_upload() {
        return average_latency_upload;
    }

    public void setAverage_latency_upload(double average_latency_upload) {
        this.average_latency_upload = average_latency_upload;
    }

    public double getTotal_latency_download() {
        return total_latency_download;
    }

    public void setTotal_latency_download(double total_latency_download) {
        this.total_latency_download = total_latency_download;
    }

    public double getAverage_latency_download() {
        return average_latency_download;
    }

    public void setAverage_latency_download(double average_latency_download) {
        this.average_latency_download = average_latency_download;
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

    public int getTotal_files_uploaded() {
        return total_files_uploaded;
    }

    public void setTotal_files_uploaded(int total_files_uploaded) {
        this.total_files_uploaded = total_files_uploaded;
    }

    public int getTotal_files_downloaded() {
        return total_files_downloaded;
    }

    public void setTotal_files_downloaded(int total_files_downloaded) {
        this.total_files_downloaded = total_files_downloaded;
    }
}
