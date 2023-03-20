//HTTP 모듈 불러오기
var http = require("http");

http.createServer(function (request, response){

    //Content Type 정의 선언
    response.writeHead(200, {'Content-Type' : 'text/plain'});

    //리스펀스를 Hello World 로 한다.
    response.end('Hello world＼\n');
}).listen(8000);

console.log('Server running at http://127.0.0.1:8000')