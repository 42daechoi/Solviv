using System;
using System.Collections.Generic;

public class PasswordGenerator
{
    private List<string> passwords;
    private HashSet<string> validPasswords;
    private Random random;

    public PasswordGenerator()
    {
        random = new Random();
        passwords = new List<string>();
        validPasswords = new HashSet<string>();

        GeneratePasswords();
    }

    private void GeneratePasswords()
    {
        while (passwords.Count < 10)
        {
            string newPassword = GenerateRandomPassword();
            if (!passwords.Contains(newPassword))
            {
                passwords.Add(newPassword);
            }
        }
        while (validPasswords.Count < 2)
        {
            int index = random.Next(0, passwords.Count);
            validPasswords.Add(passwords[index]);
        }
    }

    private string GenerateRandomPassword()
    {
        char[] password = new char[6];

        for (int i = 0; i < 6; i++)
        {
            password[i] = (char)('0' + random.Next(0, 10));
        }

        return new string(password);
    }

    public List<string> GetAllPasswords()
    {
        return new List<string>(passwords);
    }

    public bool ValidatePassword(string inputPassword)
    {
        return validPasswords.Contains(inputPassword);
    }
}
