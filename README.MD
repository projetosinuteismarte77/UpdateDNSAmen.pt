### DEPS
instalar o firefox usando a ***snap store***. No `SeleniumCode.cs` colocar o path do binario do firefox no `options.BinaryLocation` e colocar o path do binario do geckodriver que vem com o firefox no construtor do `FirefoxDriver`. usar o comando `find /snap/firefox -name geckdriver` ou firefox em vez de geckodriver para encontrar o path. **Usar o .NET6.0**

### Crontab
```0 0 * * * cd <caminho para este repo> && git pull && /usr/local/bin/dotnet build && /usr/local/bin/dotnet run```
