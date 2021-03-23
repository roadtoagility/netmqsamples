# netmqsamples


Este é um projeto de exemplo, demonstrando o uso de sockets como infraestrutura de comunicação e distribuição de processamento.
Os sockets são criados utilizando a library netmq, que é o client C# do zeromq.

# Run

Na pasta source temos outras duas pastas, Example e ZeromqHelloWorld. Na pasta Example temos uma solution com 6 projetos, que são:

* ApiExample: API dotnet core 3.1
* Broker: Responsavel pelo roteamento das mensagens, publicação nos tópicos e quaisquer decisão de infraestrutura.
* EndorsementWorker: Projeto com o handler para registros do tipo Endosso
* PolicyWorker: Projeto com o handler para registros do tipo Apólice
* Sender: Projeto console que faz o POST de um json com dados de registro de apólice de seguros, o POST é feito na API
* Shared: Biblioteca compartilhada 

Na pasta ZeromqHelloWorld temos um projeto console dotnet core 3.1, com alguns examples do básico do ZeroMQ, fonte: NetMq

# Stack

Asp.Net Core v3.1
Netmq 
