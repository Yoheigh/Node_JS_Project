// 익스프레스 모듈 불러오기
var express = require('express')
var router = express.Router();

// /경로에 해당 Wiki Home Page 출력
router.get('/', function(req , res){
    res.send('Wiki home page')
});

// /about 경로에 About this wiki Page 출력 
router.get('/about', function(req , res){
    res.send('About this wiki')
});

module.exports = router;