using System.IO;
using UpdateAmenDNSSelenium;
string user = "", password = "", ficheiroCreds = "login.txt";
if (!File.Exists("login.txt"))
{
    using (StreamWriter sw = File.CreateText(ficheiroCreds))
    {
        sw.WriteLine("<user>");
        sw.WriteLine("<pass>");
    }
    Console.WriteLine("Substituir <user> e <pass> com as credenciais corretas no login.txt");
    return;
} else
{
    var linhas = File.ReadAllLines(ficheiroCreds);
    user = linhas[0];
    password = linhas[1];
}
CreateOrUpdateCSV.Execute();

try
{
    SeleniumCode.UpdateDNS(false, user, password);
}catch(Exception e)
{
    Console.WriteLine(e.Message);
}