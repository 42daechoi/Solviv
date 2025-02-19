using UnityEngine;

[CreateAssetMenu(fileName = "PasswordPaper", menuName = "ScriptableObjects/PasswordPaper")]
public class PasswordPaper : Item
{
    [SerializeField] private string password;

    public void SetPassword(string newPassword)
    {
        password = newPassword;
    }

    public string GetPassword()
    {
        return password;
    }

    public override void UseItem()
    {
        Debug.Log($"PasswordPaper : ��й�ȣ�� {password} �Դϴ�.");
    }
}