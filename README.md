# ProjetoLabSoftware

Passo 1: Baixar os repositórios do GitHUb. \
Passo 2: Configurar a connection string no caminho ProjetoLabSoftware/Api/Api.Data/Context/ContextFactory.cs \
Passo 3: Configurar a connection string no caminho ProjetoLabSoftware/Api/Api.Aplication/appsettings.json \
Passo 4: Alterar as "TokenConfigurations" de acordo com a necessidade, abaixo se encontra o json com as configs \

"ConnectionStrings": {\
  "DefaultConnection": "server=dbapiinova.czdgknerwits.us-east-2.rds.amazonaws.com;port=3306;database=dbapiinova;uid=dbapiinova;password=rootinova123"\
},\
"TokenConfigurations": {\
  "Audience": "ExemploAudience",\
  "Issuer": "ExemploIssuer",\
  "Seconds": 172800\
}\
