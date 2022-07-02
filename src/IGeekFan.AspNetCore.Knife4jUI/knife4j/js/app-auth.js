document.getElementById("body").style.display = "none";
axios.get('/knife4j-auth').then(function (res) {
    if (res.data.result == true) {
        auth();
    } else {
        document.getElementById("body").style.display = ""
    }
});
function auth() {
    let password = prompt("请输入访问密码：", "");
    axios.get('/knife4j-pass', {
        params: {
            Password: password
        }
    }).then(function (res) {
        if (res.data.result == false) {
            auth();
        } else {
            document.getElementById("body").style.display = ""
        }
    }).catch(function (error) {
        auth();
    })
}