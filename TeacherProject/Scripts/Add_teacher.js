




function Add_teacher() {


    let xhhr = new XMLHttpRequest();
    let url = "http://localhost:62808/api/TeacherData/Create/"
    xhhr.open("POST", url, true);
    xhhr.setRequestHeader("Content-Type","application/json")


  
    let f_name = document.getElementById("fname").value;
    let l_name = document.getElementById("lname").value;
    let employeeno = document.getElementById("employeeno").value;
    let hiredate = document.getElementById("hiredate").value;
    let salary = document.getElementById("salary").value;

    var TeacherData = {
        "fname": f_name,
        "lname": l_name,
        "employeeno": employeeno,
        "hiredate": hiredate,
        "salary": salary

    };

    xhhr.onreadystatechange = function () {
        if (this.readyState === 4) {
            if (this.status === 200) {
                // let data = JSON.parse(xhhr.responseText);
  
            }
          /*  else {
                console.log()
            } */
        }
    }
    console.log(JSON.stringify(TeacherData));
   
    xhhr.send(JSON.stringify(TeacherData));
  
}