using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public struct File{
    public string name;
    public File(string name){
        this.name = name;
    }
}
public class Directory{
    public Directory[] dirs;
    public File[] files;
    public string name;
    public Directory(string name){
        this.name = name;
    }
    public void add(Directory dir){
        Array.Resize(ref this.dirs, this.dirs.Length + 1);
        this.dirs[this.dirs.GetUpperBound(0)] = dir;
    }
    public void add(File file){
        Array.Resize(ref this.dirs, this.dirs.Length + 1);
        this.files[this.dirs.GetUpperBound(0)] = file;
    }
}

public class fileSystem{
    public Directory root;
    public fileSystem(string name){
        this.root = new Directory(name);
    }
}
public enum Privilege // Changed enum name to start with a capital letter
{
    User,
    Manager,
    Admin,
    God
}

public class Account
{
    public string name;
    private string password;
    public Privilege privilege = Privilege.User; // Corrected the assignment of enum member
    public fileSystem fileSys;

    public Account(string name, Privilege privilege, Accounts root){
        this.name = name;
        this.privilege = privilege;
        this.fileSys = new fileSystem(name);
        root.root.add(this.fileSys.root);
    }
    public bool logIn(string password){
        if(this.password == password){
            return true;
        }
        return false;
    }
}

public class Accounts : MonoBehaviour
{
    // Start is called before the first frame update
    public Account[] accounts;
    public Directory root;
    void Start()
    {
        root = new Directory("C:");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addAccount(string name, Privilege privilege){
        Array.Resize(ref this.accounts, this.accounts.Length + 1);
        this.accounts[this.accounts.GetUpperBound(0)] = new Account(name, privilege, this);
    } 
}
