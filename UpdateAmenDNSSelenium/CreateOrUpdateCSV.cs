using System;
using System.Collections;
using DnsClient;

namespace UpdateAmenDNSSelenium
{
	public class Record
	{
		public string NOME, TIPO, VALOR;
		public static string MX = "MX", A = "A", AAAA = "AAAA", SOA = "SOA", NS = "NS", SRV = "SRV", ANAME = "ANAME", CNAME = "CNAME", TXT = "TXT", PTR = "PTR", SPF = "SPF";
		public int TTL;

		public Record(string nome, string tipo, string valor, int ttl = 900)
		{
			NOME  = nome;
            if (!NOME.EndsWith("."))
                NOME = NOME + ".";
            VALOR = valor;
            TIPO  = tipo;
            TTL   = ttl;
        }
        override
        public string ToString()
        {
            return "\"" + NOME + "\"," + TTL + ",\"" + TIPO + "\",\"" + VALOR + "\"";
        }
	}

	public class CreateOrUpdateCSV
	{
		public static string dnsFile = Path.GetFullPath("dns.csv");
		public List<Record> listaRecords = new List<Record>();

		public static void Execute()
		{
            var obj = new CreateOrUpdateCSV();
            //if (!File.Exists(dnsFile))
            //{//NAO ESTA A CRIAR O FICHEIRO
            //    var f = File.CreateText(dnsFile, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None);
            //    f.Close();
            //}
            var fich = File.CreateText(dnsFile);
            var encoding = new System.Text.UTF8Encoding(true);
            //fich.WriteLine("\"NOME\", \"TTL\", \"TIPO\", \"VALOR\"");
            foreach (var record in obj.listaRecords)
            {
                fich.WriteLine(record.ToString());
            }
            Console.WriteLine("Escrevi Ficheiro CSV");
            fich.Close();
		}

        public CreateOrUpdateCSV()
        {
            var lookup = new LookupClient();
            var ipResult = lookup.Query("martinho.dynip.sapo.pt", QueryType.A);
            var myip = (ipResult.Answers.First() as DnsClient.Protocol.ARecord).Address.ToString();
            Console.WriteLine("IP martinho.dynip.sapo.pt = " + myip);
            listaRecords.Add(new Record("martinho.pt.", Record.MX, "10 mail-pt.securemail.pro."));
            listaRecords.Add(new Record("smtp.martinho.pt.", Record.CNAME, "smtp-pt.securemail.pro."));
            listaRecords.Add(new Record("webmail.martinho.pt.", Record.CNAME, "webmail-pt.setupdns.net."));
            listaRecords.Add(new Record("mail.martinho.pt.", Record.CNAME, "mail-pt.securemail.pro."));
            listaRecords.Add(new Record("www.martinho.pt.", Record.CNAME, "martinho.pt."));
            listaRecords.Add(new Record("ftp.martinho.pt.", Record.CNAME, "martinho.pt."));
            listaRecords.Add(new Record("pim.martinho.pt.", Record.CNAME, "pim-pt.webapps.net."));
            listaRecords.Add(new Record("martinho.pt.", Record.TXT, "\"v=spf1 include:spf.webapps.net ~all\""));
            listaRecords.Add(new Record("autoconfig.martinho.pt.", Record.CNAME, "tb-pt.securemail.pro."));
            listaRecords.Add(new Record("_autodiscover._tcp.martinho.pt.", Record.SRV, "10 10 443 ms-pt.securemail.pro."));
            listaRecords.Add(new Record("martinho.pt.", Record.A, myip));
            listaRecords.Add(new Record("key_1catj6bqv1._domainkey.martinho.pt.", Record.TXT, "\"v=DKIM1; p=MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA2I8hbgQv1TyGrJkE+klIYLY2+2/ckzpQNTSTffXZdfE7xILrFRODeaHHPBGRoEJKu+C68BspoDDtIF4BUKz8LdNWG7jmPl2qXHdmB/oPagFZNQiQAja31pqdz7xdw2BE/W1aCTL5dQ+x9RY4+d8M987j8nSbpZxncwiEiDXKGQ5q2DGqTIz7DBYs1PNaYrz187C0bIceo1/imo+AG0oixHrhaslbFWnNgPqN9XrqLTwhe3RnbhQ+InKmXcNYpCgLUDPAQXX/arnAh8cy6f2mgThBNTso0p4SOBPyQVo7yChi3Di465KrXEV21eICi3uJpHGMkys/ZMZbNjIy3Xt9iwIDAQAB\""));
            listaRecords.Add(new Record("homeassistant.martinho.pt.", Record.A, myip));
            listaRecords.Add(new Record("swag.martinho.pt.", Record.A, myip));
            listaRecords.Add(new Record("_atproto", Record.TXT, "did=did:plc:5yscnem3yijhdwwh4nwed6ut"));
            listaRecords.Add(new Record("vpscs.martinho.pt", Record.A, "45.13.119.86"));
            listaRecords.Add(new Record("testingings.martinho.pt", Record.A, "1.1.1.1"));
        }
    }
}

