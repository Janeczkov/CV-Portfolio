package com.example.serwer.controller;


import com.example.serwer.exception.ResourceNotFoundException;
import com.example.serwer.model.Accounts;
import com.example.serwer.repository.AccountsRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;


import javax.validation.Valid;
import java.util.List;

@RestController
//@RequestMapping("/accounts")
public class AccountsController {

    @Autowired
    private AccountsRepository accountsRepository;

    @GetMapping("/accounts")
    public List<Accounts> getAllAccounts() {
        long przed = System.currentTimeMillis();
        List<Accounts> konta = accountsRepository.findAll();
        long po = System.currentTimeMillis();
        long roznica = po - przed;
        System.out.println(przed);
        System.out.println(po);
        System.out.println(String.valueOf(roznica));
        return konta;
    }

    @GetMapping("/ping")
    public String getping() {
/*
        long przed = System.currentTimeMillis();
        List<Accounts> konta = accountsRepository.findAll();
        long po = System.currentTimeMillis();
        long roznica = po - przed;
        System.out.println(przed);
        System.out.println(po);
        System.out.println(String.valueOf(roznica));



        return "wartosc1: " + String.valueOf(przed) + System.lineSeparator() + "wartosc2: " + String.valueOf(po) + System.lineSeparator() + "czas odpowiedzi: " + String.valueOf(roznica);
  */
return "";
    }

    @PostMapping("/accounts")
    public Accounts createAccount(@Valid @RequestBody Accounts account) {
        account.setRank("3");
        return accountsRepository.save(account);
    }

    @GetMapping("/accounts/{id}")
    public Accounts getAccountById(@PathVariable(value = "id") Long accountId) {
        return accountsRepository.findById(accountId)
                .orElseThrow(() -> new ResourceNotFoundException("Note", "id", accountId));
    }

    @GetMapping("/accounts/username/{username}")
    public Accounts getAccountByUsername(@PathVariable(value = "username") String username) {
        return accountsRepository.findByUsername(username).get(0);
                //.orElseThrow(() -> new ResourceNotFoundException("Note", "id", accountId));
    }

    @PutMapping("/accounts/{id}")
    public Accounts updateAccount(@PathVariable(value = "id") Long accountId,
                                  @Valid @RequestBody Accounts accountDetails) {

        Accounts account = accountsRepository.findById(accountId)
                .orElseThrow(() -> new ResourceNotFoundException("Account", "id", accountId));

        //
        account.setRank(accountDetails.getRank());
        int curelements = account.getNumber_of_files_uploaded();
        account.setNumber_of_files_uploaded(curelements + accountDetails.getNumber_of_files_uploaded());


        Accounts updatedAccounts = accountsRepository.save(account);
        return updatedAccounts;
    }

    @DeleteMapping("/accounts/{id}")
    public ResponseEntity<?> deleteAccount(@PathVariable(value = "id") Long accountId) {
        Accounts account = accountsRepository.findById(accountId)
                .orElseThrow(() -> new ResourceNotFoundException("Account", "id", accountId));

        accountsRepository.delete(account);

        return ResponseEntity.ok().build();
    }


}