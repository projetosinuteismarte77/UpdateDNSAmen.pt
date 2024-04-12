## Porque?

PORQUE O AMEN.PT **RECUSA-SE** A FAZER UM API COMO O GODADDY. Eu perguntei-lhes e eles disseram que não havia necessidade. muito bem adorei 👍

### DEPS
instalar o firefox usando a ***snap store***. No `SeleniumCode.cs` colocar o path do binario do firefox no `options.BinaryLocation` e colocar o path do binario do geckodriver que vem com o firefox no construtor do `FirefoxDriver`. usar o comando `find /snap/firefox -name geckdriver` ou firefox em vez de geckodriver para encontrar o path. **Usar o .NET6.0**

### Crontab
```0 0 * * * cd <caminho para este repo> && git pull && /usr/local/bin/dotnet build && /usr/local/bin/dotnet run > /dev/null```

### Outros
Ao correr pela primeira vez, o script vai criar um ficheiro na pasta de build chamada `login.txt`, dentro deste devemos substituir os <user> e <password> pelas credenciais do amen.
