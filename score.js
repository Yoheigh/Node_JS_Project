const express = require('express');
const app = express();

let users = [];                                     //App.js에서 users 배열 추가

app.use(express.json());

app.get('/', (req, res)=>{
    res.send('hello world!');
});

app.get('/scores/:id', (req, res)=>{
    console.log('id : ' + req.params.id);               //유니티에서 req에 id를 무엇을 보냈는지 확인
    let user = users.find(x=>x.id == req.params.id);    //배열 값에 유저가 있나 확인
    
    if (user === undefined)                             // if(user == null) => if (user === undefined)
    {
        res.send({
            cmd : 1103,
            message : '잘못된 id 입니다.'
        });
    }
    else
    {
        res.send({
            cmd : 1102,
            message : '',
            result : user
        });
    }

    res.send('hello world!');
});

app.get('/scores/top3', (req, res)=>{
    let result = users.sort(function (a,b) {            //Users를 sort
        return b.score - b.score;
    })
    result = result.slice(0,3);                         // 0 ~ (n - 1) 1~3등을 잘라서 보여준다.

    res.send({
        cmd : 1101,
        message : '',
        result
    });
});

app.post('/scores', (req, res)=>{
    const {id , score} = req.body;

    console.log(req.body);

    let result = {
        cmd : -1,
        message : '',
    };

    let user = users.find(x=>x.id == id);

    if(user === undefined)
    {
        users.push({id , score});
        result.cmd = 1001;
        result.message = '점수가 신규 등록되었습니다.'
    }
    else
    {
        console.log(score , id , user.score);

        if(score > user.score)
        {
            user.score = score;
            result.cmd = 1002;
            result.message = '점수가 갱신 되었습니다.'
        }
        else
        {
            result.cmd = 1003;
        }
    }
    console.log(users);
    res.send(result);
});

app.listen(3030, ()=>{
    console.log('server is running at 3030 port');
})