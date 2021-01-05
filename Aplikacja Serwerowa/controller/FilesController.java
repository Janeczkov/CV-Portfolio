package com.example.serwer.controller;

import com.example.serwer.exception.ResourceNotFoundException;
import com.example.serwer.model.Accounts;
import com.example.serwer.model.Files;
import com.example.serwer.model.ServerStats;
import com.example.serwer.property.FileStorageProperties;
import com.example.serwer.repository.AccountsRepository;
import com.example.serwer.repository.FilesRepository;
import com.example.serwer.repository.ServerStatsRepository;
import com.example.serwer.service.FileStorageService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.io.Resource;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import javax.validation.Valid;
import java.io.File;
import java.math.RoundingMode;
import java.text.DecimalFormat;
import java.util.List;

@RestController
@RequestMapping("/file")
public class FilesController {

    private static final Logger logger = LoggerFactory.getLogger(FilesController.class);


    @Autowired
    private FileStorageService fileStorageService;

    @Autowired
    private FilesRepository filesRepository;
    @Autowired
    private AccountsRepository accountsRepository;
    @Autowired
    private ServerStatsRepository serverStatsRepository;


    @PostMapping("/upload")
    public Files uploadFile(@RequestParam("content") MultipartFile newfile,
                            @RequestParam("autor") String author) {
        String fileName = fileStorageService.storeFile(newfile);


        Files file = new Files(fileName,
                newfile.getContentType(), newfile.getSize(), author);
        //NumberFormat nf = NumberFormat.getNumberInstance(Locale.GERMAN);
        DecimalFormat df = new DecimalFormat("#.#####");
        df.setRoundingMode(RoundingMode.CEILING);

        String liczbatext = df.format(newfile.getSize() * 0.00000095367432).replace(',', '.');
        file.setFile_sizeMB(Float.parseFloat(liczbatext));


        List<Files> files = filesRepository.findByFileName(fileName);

        for (Files tempfile : files) {
            filesRepository.delete(tempfile);
            FileStorageProperties fileStorageProperties = new FileStorageProperties();
            String fileStorageLocation = fileStorageProperties.getUploadDir();
            String filename = tempfile.getFileName();
            File filetodel = new File(fileStorageLocation, filename);
            filetodel.delete();
        }



        return filesRepository.save(file);
    }

    @GetMapping("/all")
    public List<Files> getAllFiles() {
        return filesRepository.findAll();
    }

    @GetMapping("/{id}")
    public Files getFileById(@PathVariable(value = "id") int id) {
        return filesRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("File", "id", id));
        //.orElseThrow(() -> new ResourceNotFoundException("Note", "id", accountId));
    }

    @PutMapping("/update/{id}")
    public Files updateFile(@PathVariable(value = "id") int fileId,
                            @Valid @RequestBody Files fileDetails) {

        Files file = filesRepository.findById(fileId)
                .orElseThrow(() -> new ResourceNotFoundException("File", "id", fileId));

        //


        Files updatedFiles = filesRepository.save(file);
        return updatedFiles;
    }

    @PutMapping("/updatestats/{id}")
    public Files updateStatsFile(@PathVariable(value = "id") int fileId,
                                 @Valid @RequestBody Files updateDetails) {

        Files file = filesRepository.findByFileName(updateDetails.getTemp_filename()).get(0);

        Accounts user = accountsRepository.findByUsername(updateDetails.getTemp_username()).get(0);
        ServerStats javastat = serverStatsRepository.findByPlatform("java").get(0);
        ServerStats csharpstat = serverStatsRepository.findByPlatform("csharp").get(0);

        System.out.println("pobrane: " + updateDetails.getTemp_upload_time() + updateDetails.getTemp_download_time() + updateDetails.getTemp_avg_latency() + updateDetails.getTemp_platform());
        System.out.println(user.getUsername());
        user.setApp_language(updateDetails.getTemp_platform());
        double total = 0;

        if (updateDetails.getTemp_download_time() > updateDetails.getTemp_upload_time()) { // download
            total = user.getTotal_time_of_downloading() + updateDetails.getTemp_download_time();
            user.setTotal_time_of_downloading(total);
            user.setNumber_of_files_downloaded(user.getNumber_of_files_downloaded() + 1);
            user.setBytes_downloaded(user.getBytes_downloaded() + file.getFile_sizeB());
            user.setMegabytes_downloaded(user.getMegabytes_downloaded() + file.getFile_sizeMB());
            user.setAverage_time_of_downloading(user.getTotal_time_of_downloading() / user.getNumber_of_files_downloaded());
            user.setTotal_latency(user.getTotal_latency() + updateDetails.getTemp_avg_latency());
            user.setAverage_latency(user.getTotal_latency() / user.getNumber_of_files_downloaded());
            user.setAverage_time_of_downloading(user.getTotal_time_of_downloading() / user.getNumber_of_files_downloaded());
            user.setRaw_average_time_of_downloading(user.getAverage_time_of_downloading() - user.getAverage_latency());

            if (updateDetails.getTemp_platform().equals("java")) {
                total = file.getTotal_time_downloaded_java() + updateDetails.getTemp_download_time();
                file.setTotal_time_downloaded_java(total);
                file.setNumber_of_downloads_java(file.getNumber_of_downloads_java() + 1);
                file.setAverage_time_downloaded_java(file.getTotal_time_downloaded_java() / file.getNumber_of_downloads_java());
                file.setTotal_latency_java(file.getTotal_latency_java() + updateDetails.getTemp_avg_latency());
                file.setAverage_latency_java(file.getTotal_latency_java() / file.getNumber_of_downloads_java());
                file.setTime_per_megabyte_download_java(file.getTotal_time_downloaded_java() / (file.getFile_sizeMB() * file.getNumber_of_downloads_java()));
                file.setRaw_average_time_downloaded_java(file.getAverage_time_downloaded_java() - file.getAverage_latency_java());
                file.setRaw_time_per_megabyte_download_java(file.getTime_per_megabyte_download_java() - file.getAverage_latency_java());

                javastat.setTotal_files_downloaded(javastat.getTotal_files_downloaded() + 1);
                javastat.setTotal_latency_download(javastat.getTotal_latency_download() + updateDetails.getTemp_avg_latency());
                javastat.setAverage_latency_download(javastat.getTotal_latency_download() / javastat.getTotal_files_downloaded());
                javastat.setTotal_B_size_of_files_downloaded(javastat.getTotal_B_size_of_files_downloaded() + file.getFile_sizeB());
                javastat.setTotal_MB_size_of_files_downloaded(javastat.getTotal_MB_size_of_files_downloaded() + file.getFile_sizeMB());
                javastat.setTotal_time_downloaded(javastat.getTotal_time_downloaded() + updateDetails.getTemp_download_time());
                javastat.setAverage_time_downloaded(javastat.getTotal_time_downloaded() / javastat.getTotal_files_downloaded());
                javastat.setRaw_average_time_downloaded(javastat.getAverage_time_downloaded() - javastat.getAverage_latency_download());
                javastat.setTime_per_megabyte_download(javastat.getTotal_time_downloaded() / javastat.getTotal_MB_size_of_files_downloaded());
                javastat.setRaw_time_per_megabyte_download(javastat.getTime_per_megabyte_download() - javastat.getAverage_latency_download());


            } else if (updateDetails.getTemp_platform().equals("csharp")) {
                //1MB = 1048576B

                total = file.getTotal_time_downloaded_csharp() + updateDetails.getTemp_download_time();
                file.setTotal_time_downloaded_csharp(total);
                file.setNumber_of_downloads_csharp(file.getNumber_of_downloads_csharp() + 1);
                file.setAverage_time_downloaded_csharp(file.getTotal_time_downloaded_csharp() / file.getNumber_of_downloads_csharp());
                file.setTotal_latency_csharp(file.getTotal_latency_csharp() + updateDetails.getTemp_avg_latency());
                file.setAverage_latency_csharp(file.getTotal_latency_csharp() / file.getNumber_of_downloads_csharp());
                file.setTime_per_megabyte_download_csharp(file.getTotal_time_downloaded_csharp() / (file.getFile_sizeMB() * file.getNumber_of_downloads_csharp()));
                file.setRaw_average_time_downloaded_csharp(file.getAverage_time_downloaded_csharp() - file.getAverage_latency_csharp());
                file.setRaw_time_per_megabyte_download_csharp(file.getTime_per_megabyte_download_csharp() - file.getAverage_latency_csharp());

                csharpstat.setTotal_files_downloaded(csharpstat.getTotal_files_downloaded() + 1);
                csharpstat.setTotal_latency_download(csharpstat.getTotal_latency_download() + updateDetails.getTemp_avg_latency());
                csharpstat.setAverage_latency_download(csharpstat.getTotal_latency_download() / csharpstat.getTotal_files_downloaded());
                csharpstat.setTotal_B_size_of_files_downloaded(csharpstat.getTotal_B_size_of_files_downloaded() + file.getFile_sizeB());
                csharpstat.setTotal_MB_size_of_files_downloaded(csharpstat.getTotal_MB_size_of_files_downloaded() + file.getFile_sizeMB());
                csharpstat.setTotal_time_downloaded(csharpstat.getTotal_time_downloaded() + updateDetails.getTemp_download_time());
                csharpstat.setAverage_time_downloaded(csharpstat.getTotal_time_downloaded() / csharpstat.getTotal_files_downloaded());
                csharpstat.setRaw_average_time_downloaded(csharpstat.getAverage_time_downloaded() - csharpstat.getAverage_latency_download());
                csharpstat.setTime_per_megabyte_download(csharpstat.getTotal_time_downloaded() / csharpstat.getTotal_MB_size_of_files_downloaded());
                csharpstat.setRaw_time_per_megabyte_download(csharpstat.getTime_per_megabyte_download() - csharpstat.getAverage_latency_download());
            }
            user.setTime_per_megabyte_download(user.getTotal_time_of_downloading() / user.getMegabytes_downloaded());
            user.setRaw_time_per_megabyte_download(user.getTime_per_megabyte_download() - user.getAverage_latency());
        } else if (updateDetails.getTemp_download_time() < updateDetails.getTemp_upload_time()) { // upload

            total = user.getTotal_time_of_uploading() + updateDetails.getTemp_upload_time();
            user.setTotal_time_of_uploading(total);
            user.setNumber_of_files_uploaded(user.getNumber_of_files_uploaded() + 1);
            user.setBytes_uploaded(user.getBytes_uploaded() + file.getFile_sizeB());
            user.setMegabytes_uploaded(user.getMegabytes_uploaded() + file.getFile_sizeMB());
            user.setAverage_time_of_uploading(user.getTotal_time_of_uploading() / user.getNumber_of_files_uploaded());
            user.setTotal_latency(user.getTotal_latency() + updateDetails.getTemp_avg_latency());
            user.setAverage_latency(user.getTotal_latency() / (user.getNumber_of_files_downloaded() + user.getNumber_of_files_uploaded()));
            user.setRaw_average_time_of_uploading(user.getAverage_time_of_uploading() - user.getAverage_latency());


            if (updateDetails.getTemp_platform().equals("java")) {
                javastat.setTotal_files_uploaded(javastat.getTotal_files_uploaded() + 1);
                javastat.setTotal_latency_upload(javastat.getTotal_latency_upload() + updateDetails.getTemp_avg_latency());
                javastat.setAverage_latency_upload(javastat.getTotal_latency_upload() / javastat.getTotal_files_uploaded());
                javastat.setTotal_B_size_of_files_uploaded(javastat.getTotal_B_size_of_files_uploaded() + file.getFile_sizeB());
                javastat.setTotal_MB_size_of_files_uploaded(javastat.getTotal_MB_size_of_files_uploaded() + file.getFile_sizeMB());
                javastat.setTotal_time_uploaded(javastat.getTotal_time_uploaded() + updateDetails.getTemp_upload_time());
                javastat.setAverage_time_uploaded(javastat.getTotal_time_uploaded() / javastat.getTotal_files_uploaded());
                javastat.setRaw_average_time_uploaded(javastat.getAverage_time_uploaded() - javastat.getAverage_latency_upload());
                javastat.setTime_per_megabyte_upload(javastat.getTotal_time_uploaded() / javastat.getTotal_MB_size_of_files_uploaded());
                javastat.setRaw_time_per_megabyte_upload(javastat.getTime_per_megabyte_upload() - javastat.getAverage_latency_upload());


            } else if (updateDetails.getTemp_platform().equals("csharp")) {
                csharpstat.setTotal_files_uploaded(csharpstat.getTotal_files_uploaded() + 1);
                csharpstat.setTotal_latency_upload(csharpstat.getTotal_latency_upload() + updateDetails.getTemp_avg_latency());
                csharpstat.setAverage_latency_upload(csharpstat.getTotal_latency_upload() / csharpstat.getTotal_files_uploaded());
                csharpstat.setTotal_B_size_of_files_uploaded(csharpstat.getTotal_B_size_of_files_uploaded() + file.getFile_sizeB());
                csharpstat.setTotal_MB_size_of_files_uploaded(csharpstat.getTotal_MB_size_of_files_uploaded() + file.getFile_sizeMB());
                csharpstat.setTotal_time_uploaded(csharpstat.getTotal_time_uploaded() + updateDetails.getTemp_upload_time());
                csharpstat.setAverage_time_uploaded(csharpstat.getTotal_time_uploaded() / csharpstat.getTotal_files_uploaded());
                csharpstat.setRaw_average_time_uploaded(csharpstat.getAverage_time_uploaded() - csharpstat.getAverage_latency_upload());
                csharpstat.setTime_per_megabyte_upload(csharpstat.getTotal_time_uploaded() / csharpstat.getTotal_MB_size_of_files_uploaded());
                csharpstat.setRaw_time_per_megabyte_upload(csharpstat.getTime_per_megabyte_upload() - csharpstat.getAverage_latency_upload());
                System.out.println("heck2");
            }
            user.setTime_per_megabyte_upload(user.getTotal_time_of_uploading() / user.getMegabytes_uploaded());
            user.setRaw_time_per_megabyte_upload(user.getTime_per_megabyte_upload() - user.getAverage_latency());

        }


        Files updatedFiles = filesRepository.save(file);
        accountsRepository.save(user);
        return updatedFiles;
    }


    @GetMapping("/reset/{id}")
    public Files resetStats(@PathVariable(value = "id") int fileId) {
        return null;
    }

    @GetMapping("/download/{id}")
    public ResponseEntity<Resource> downloadFile(@PathVariable(value = "id") int fileId) {
        // Load file as Resource

        Files file = filesRepository.findById(fileId)
                .orElseThrow(() -> new ResourceNotFoundException("File", "id", fileId));


        Resource resource = fileStorageService.loadFileAsResource(file.getFileName());


        String contentType = "application/octet-stream";


        return ResponseEntity.ok()
                .contentType(MediaType.parseMediaType(contentType))
                .header(HttpHeaders.CONTENT_DISPOSITION, "attachment; filename=\"" + resource.getFilename() + "\"")
                .body(resource);
    }

    @DeleteMapping("/delete/{id}")
    public ResponseEntity<?> deleteAccount(@PathVariable(value = "id") int fileId,
                                           FileStorageProperties fileStorageProperties) {
        Files file = filesRepository.findById(fileId)
                .orElseThrow(() -> new ResourceNotFoundException("File", "id", fileId));

        filesRepository.delete(file);

        String fileStorageLocation;

        fileStorageLocation = fileStorageProperties.getUploadDir();

        String filename = file.getFileName();
        File newfile = new File(fileStorageLocation, filename);

        newfile.delete();

        return ResponseEntity.ok().build();
    }
}