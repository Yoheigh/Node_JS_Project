const express = require('express');
const app = express();

app.use(express.json());

app.get('/', (req, res)=>{
    res.send('hello world!')
});

app.listen(3030, ()=> {
    console.log('server is running at 3030 port');
})