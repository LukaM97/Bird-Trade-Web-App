var host = "https://localhost:";
var port = "44320/";
var birdsEndpoint = "api/Birds/";
var ordersEndpoint = "api/Orders/";
var loginEndpoint = "api/Account/login";
var registerEndpoint = "api/Account/register";
var formAction = "Create";
var editingId;
var jwt_token;

function showLogin(){
    document.getElementById("loginForm").style.display = "block";
    document.getElementById("registerForm").style.display = "none";
    document.getElementById("index").style.display = "none";
	document.getElementById("data").style.display = "none";
}

function showRegistration(){
    document.getElementById("loginForm").style.display = "none";
    document.getElementById("registerForm").style.display = "block";
    document.getElementById("index").style.display = "none";
	document.getElementById("data").style.display = "none";
}

function quitForm(){
    document.getElementById("loginForm").style.display = "none";
    document.getElementById("registerForm").style.display = "none";
    document.getElementById("index").style.display = "block";
	document.getElementById("data").style.display = "block";
}

function birdForm(){
	document.getElementById("pretragaForm").style.display = "none";
	document.getElementById("createForm").style.display = "none";
	document.getElementById("data").style.display = "none";
	document.getElementById("birdLink").style.display = "none";
	document.getElementById("btnAfter").style.display = "none";
    document.getElementById("infoForm").style.display = "block";
	loadBirds(setBirds2);
	document.getElementById("createBirdForm").style.display = "block";
	document.getElementById("backButtonForm").style.display = "block";
}

function orderForm(){
	document.getElementById("index").style.display = "none";
	document.getElementById("data").style.display = 'none';
	document.getElementById("createForm").style.display = "block";

	var birdID = this.option;
	var birdName = this.name;
	var dropdown = document.getElementById("orderBird");
	dropdown.innerHTML = "";

	var option = document.createElement("option");
	option.value = birdID;
	var text = document.createTextNode(birdName);

	option.appendChild(text);
	dropdown.appendChild(option);
	console.log(birdID);
	console.log(birdName);
}

function afterLogIn(){
	document.getElementById("infoForm").style.display = "block";
	document.getElementById("backButtonForm").style.display = "inline-block";
	document.getElementById("loginForm").style.display = "none";
	document.getElementById("data").innerHTML = "";
	document.getElementById("data").style.display = "block";
	loadOrders();
	loadBirds(setBirds);
	if(isAdmin()){
		document.getElementById("pretragaForm").style.display = "block";
	}
	document.getElementById("createForm").style.display = "block";
	document.getElementById("backBtn").style.display = "none";
	document.getElementById("backBtn2").style.display = "none";
	document.getElementById("index").style.display = "none";
	document.getElementById("birdLink").style.display = "none";
}

function isAdmin(){
	if (jwt_token) {
		console.log("type:" + typeof(jwt_token));
		const role = JSON.parse(window.atob(jwt_token.split(".")[1])).role;
		if(role == "Admin"){
			return true;
		}

		return false;
	}
}

function loadOrders(){
    var requestUrl = host + port + ordersEndpoint;
    var headers = { 'Content-Type': 'application/json' };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;		
	}
	console.log("Request URL: " + requestUrl);
	fetch(requestUrl,{headers:headers})
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setOrders);
			} else {
				console.log("Error occured with code " + response.status);
			}
		})
		.catch(error => console.log(error));
}

function loadBirds(setBirds) {
	var requestUrl = host + port + birdsEndpoint;
	console.log("Request URL: " + requestUrl);
	fetch(requestUrl)
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setBirds);
			} else {
				console.log("Error occured with code " + response.status);
			}
		})
		.catch(error => console.log(error));
};

function setOrders(data){
    var container = document.getElementById("data");

    console.log(data);

    var h1 = document.createElement("h1");
    container.appendChild(h1);
	h1.innerHTML = "Porudzbine";
    container.appendChild(document.createElement("br"));
    var table = document.createElement("table");
	table.className = "table table-hover";
    table.style = "width: 60%";
    var tableBody = document.createElement("tbody");
    var tableHeader = document.createElement("thead");
	tableHeader.className = "prviHeader";
    
	var row = document.createElement("tr");
    if(jwt_token){
			
			var th1 = document.createElement("th");
			var th1Text = document.createTextNode("Ime");
			th1.appendChild(th1Text);
			var th2 = document.createElement("th");
			var th2Text = document.createTextNode("Prezime");
			th2.appendChild(th2Text);
			var th3 = document.createElement("th");
			var th3Text = document.createTextNode("Adresa");
			th3.appendChild(th3Text);
			var th4 = document.createElement("th");
			var th4Text = document.createTextNode("Grad");
			th4.appendChild(th4Text);
			var th5 = document.createElement("th");
			var th5Text = document.createTextNode("Kolicina");
			th5.appendChild(th5Text);
			row.appendChild(th1);
			row.appendChild(th2);
			row.appendChild(th3);
			row.appendChild(th4);
			row.appendChild(th5);
            var th6 = document.createElement("th") 
	        var th6Text = document.createTextNode("Korisnicki ID");
	        th6.appendChild(th6Text);
            row.appendChild(th6);
            var th7 = document.createElement("th") 
	        var th7Text = document.createTextNode("Vrsta");
	        th7.appendChild(th7Text);
            row.appendChild(th7);
			var th8 = document.createElement("th") 
	        var th8Text = document.createTextNode("Pol");
	        th8.appendChild(th8Text);
            row.appendChild(th8);
    }

	tableHeader.appendChild(row);

    for(var i=0; i<data.length; i++){
        var row1 = document.createElement("tr");

        if (jwt_token) {
			var td1 = document.createElement("td");
			var td1Text = document.createTextNode(data[i].nameOfCustomer);
			td1.appendChild(td1Text);
			var td2 = document.createElement("td");
			var td2Text = document.createTextNode(data[i].surnameOfCustomer);
			td2.appendChild(td2Text);
			var td3 = document.createElement("td");
			var td3Text = document.createTextNode(data[i].adressOfCustomer);
			td3.appendChild(td3Text);
			var td4 = document.createElement("td");
			var td4Text = document.createTextNode(data[i].customerCity);
			td4.appendChild(td4Text);
			var td5 = document.createElement("td");
			var td5Text = document.createTextNode(data[i].amount);
			td5.appendChild(td5Text);
			var td6 = document.createElement("td");
			var td6Text = document.createTextNode(data[i].customerIdNumber);
			td6.appendChild(td6Text);
			var td7 = document.createElement("td");
			var td7Text = document.createTextNode(data[i].bird.name);
			td7.appendChild(td7Text);

			row1.appendChild(td1);
			row1.appendChild(td2);
			row1.appendChild(td3);
			row1.appendChild(td4);
			row1.appendChild(td5);
			row1.appendChild(td6);
			row1.appendChild(td7);
            var td8 = document.createElement("td");
			if(data[i].bird.genderId == 2){
				var td8Text = document.createTextNode("Muzjak");
				td8.appendChild(td8Text);
				row1.appendChild(td8);
			}else{
				var td8Text = document.createTextNode("Zenka");
				td8.appendChild(td8Text);
				row1.appendChild(td8);
			}

            var stringId = data[i].id.toString();

            var buttonDelete = document.createElement("button");
            buttonDelete.name = stringId;
            buttonDelete.addEventListener("click", deleteOglas);
            var buttonDeleteText = document.createTextNode("obrisi");
            buttonDelete.appendChild(buttonDeleteText);
            var buttonDeleteCell = document.createElement("td");
            buttonDeleteCell.appendChild(buttonDelete);
            buttonDelete.style.backgroundColor = "red";
            row1.appendChild(buttonDeleteCell);	

        }

        tableBody.appendChild(row1);
    }
    
    table.appendChild(tableHeader);
    table.appendChild(tableBody);

    
    container.appendChild(table);
}

function setBirds(data) {
	var dropdown = document.getElementById("orderBird");
	dropdown.innerHTML = "";
	for (var i = 0; i < data.length; i++) {
		var option = document.createElement("option");
		option.value = data[i].id;
		var text = document.createTextNode(data[i].name + " (");
		if(data[i].genderId == 2){
			text.appendData("M)");
		}
		else{
			text.appendData("Z)");
		}
		text.appendData(data[i].quantity);
		option.appendChild(text);
		dropdown.appendChild(option);
	}
}

function setBirds2(data){
	var container = document.getElementById("data");
	container.innerHTML = "";

	console.log(data);

    var h1 = document.createElement("h1");
    container.appendChild(h1);
	h1.innerHTML = "Ptice";
    container.appendChild(document.createElement("br"));
    var table = document.createElement("table");
	table.className = "table table-hover";
    table.style = "width: 60%";
    var tableBody = document.createElement("tbody");
    var tableHeader = document.createElement("thead");
	tableHeader.className = "prviHeader";
    
	var row = document.createElement("tr");
    		
	var th1 = document.createElement("th") 
	var th1Text = document.createTextNode("Naziv");
	th1.appendChild(th1Text);
	var th2 = document.createElement("th")
	var th2Text = document.createTextNode("Kolicina");
	th2.appendChild(th2Text);
	var th3 = document.createElement("th") 
	var th3Text = document.createTextNode("Pol");
	th3.appendChild(th3Text);
	var th4 = document.createElement("th")
	row.appendChild(th1);
	row.appendChild(th2);
	row.appendChild(th3);
	row.appendChild(th4);
			
	tableHeader.appendChild(row);

    for(var i=0; i<data.length; i++){
        var row1 = document.createElement("tr");

		var td1 = document.createElement("td");
		var td1Text = document.createTextNode(data[i].name);
		td1.appendChild(td1Text);
		var td2 = document.createElement("td");
		var td2Text = document.createTextNode(data[i].quantity);
		td2.appendChild(td2Text);
		var td3 = document.createElement("td");
		row1.appendChild(td1);
		row1.appendChild(td2);
		if(data[i].gender.id == 1){
			var td3Text = document.createTextNode("Zenka");
			td3.appendChild(td3Text);
			row1.appendChild(td3);
		}else{
			var td3Text = document.createTextNode("Muzjak");
			td3.appendChild(td3Text);
			row1.appendChild(td3);
		}
		
		var buttonOrder = document.createElement("button");
		if(data[i].gender.id == 1){
			buttonOrder.name = data[i].name + "(Z)" + data[i].quantity;
		}else{
			buttonOrder.name = data[i].name + "(M)" + data[i].quantity;
		}
		buttonOrder.option = data[i].id;
		buttonOrder.addEventListener("click", orderForm);
		var buttonOrderText = document.createTextNode("Poruci");
		buttonOrder.appendChild(buttonOrderText);
		var buttonOrderCell = document.createElement("td");
		buttonOrderCell.appendChild(buttonOrder);
		buttonOrder.style.backgroundColor = "gray";
		row1.appendChild(buttonOrderCell);
		
		var stringId = data[i].id.toString();
		if (isAdmin()) {
			var buttonAdd = document.createElement("button");
			buttonAdd.name = stringId;
			buttonAdd.addEventListener("click", addBirdQuantity);
			var buttonAddText = document.createTextNode("+");
			buttonAdd.appendChild(buttonAddText);
			var buttonAddCell = document.createElement("td");
			buttonAddCell.appendChild(buttonAdd);
			buttonAdd.style.backgroundColor = "green";
			row1.appendChild(buttonAddCell);
			buttonOrderCell.style.display = "none";
		}

		tableBody.appendChild(row1);
    }
    
    table.appendChild(tableHeader);
    table.appendChild(tableBody);

    container.appendChild(table);
}

function validateRegisterForm(username,email,password,confirmPassword){
    var usernameError = document.getElementById("usernameRegError");
    usernameError.innerHTML = "";
    var emailError = document.getElementById("emailRegError");
    emailError.innerHTML = "";
    var passwordError = document.getElementById("passwordRegError");
    passwordError.innerHTML = "";
    var confirmPassError = document.getElementById("confirmPassRegError");
    confirmPassError.innerHTML = "";

    var isValid = true;
    if(!username){
        usernameError.innerHTML = "This field can not be empty!";
        isValid = false;
    }
    if(!email){
        emailError.innerHTML = "This field can not be empty!";
        isValid = false;
    }
    if(!password){
        passwordError.innerHTML = "This field can not be empty!";
        isValid = false;
    }
    if(!confirmPassword){
        confirmPassError.innerHTML = "This field can not be empty!";
        isValid = false;
    }
    else if(confirmPassword != password){
        confirmPassError.innerHTML = "The input value does not match the value of password!";
        isValid = false;
    }

    return isValid;
}

function registerUser() {
	var username = document.getElementById("usernameRegister").value;
	var email = document.getElementById("emailRegister").value;
	var password = document.getElementById("passwordRegister").value;
	var confirmPassword = document.getElementById("confirmPasswordRegister").value;

	if (validateRegisterForm(username, email, password, confirmPassword)) {
		var url = host + port + registerEndpoint;
		var sendData = { "Username": username, "Email": email, "Password": password };
		fetch(url, { method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200) {
					document.getElementById("registerForm");
					console.log("Successful registration");
					showLogin();
				} else {
					console.log("Error occured with code " + response.status);
					console.log(response);
					alert("Greska prilikom registracije!");
					response.text().then(text => { console.log(text); })
				}
			})
			.catch(error => console.log(error));
	}
	return false;
}

function validateLoginForm(username,password){
    var usernameError = document.getElementById("usernameLoginError");
    usernameError.innerHTML = "";
    var passwordError = document.getElementById("passwordLoginError");
    passwordError.innerHTML = "";

    var isValid = true;
    if(!username){
        usernameError.innerHTML = "This field can not be empty!";
        isValid = false;
    }
    if(!password){
        passwordError.innerHTML = "This field can not be empty!";
        isValid = false;
    }

    return isValid;
}

function loginUser(){
    var username = document.getElementById("usernameLogin").value;
    var password = document.getElementById("passwordLogin").value;

    if(validateLoginForm(username,password)){
        var url = host + port + loginEndpoint;
		var sendData = { "Username": username, "Password": password };
		fetch(url, { method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200) {
					console.log("Successful login");
					response.json().then(function (data) {
						console.log(data);
						document.getElementById("info").innerHTML = "Prijavljeni korisnik: <b>" + data.username + "<b/>";
						jwt_token = data.token;
                        afterLogIn();
					});
				} else {
					console.log("Error occured with code " + response.status);
					console.log(response);
					alert("Greska prilikom prijave!");
				}
            })
			.catch(error => console.log(error));
    }
    return false;
}

function validateOrder(name,surname,adress,city,amount){
    var nameError = document.getElementById("nameError");
    nameError.innerHTML = "";
    var surnameError = document.getElementById("surnameError");
    surnameError.innerHTML = "";
	var adressError = document.getElementById("adressError");
    adressError.innerHTML = "";
	var cityError = document.getElementById("cityError");
    cityError.innerHTML = "";
	var amountError = document.getElementById("amountError");
    amountError.innerHTML = "";

    var isValid = true;
    if(!name){
        nameError.innerHTML = "Ovo polje ne moze ostati prazno!";
        isValid = false;
    }
	else if(name.length < 2 || name.length > 100){
		nameError.innerHTML = "Naslov moze imati najmanje 2 a najvise 100 karaktera!";
		isValid = false;
	}
    if(!surname){
        surnameError.innerHTML = "Ovo polje ne moze ostati prazno!";
        isValid = false;
    }
	else if(surname.length < 2 || surname.length > 20){
		surnameError.innerHTML = "Tip moze imati najmanje 2 a najvise 20 karaktera!";
        isValid = false;
	}
	if(!adress){
        adressError.innerHTML = "Ovo polje ne moze ostati prazno!";
        isValid = false;
    }
	if(!city){
        cityError.innerHTML = "Ovo polje ne moze ostati prazno!";
        isValid = false;
    }
	if(!amount){
        amountError.innerHTML = "Ovo polje ne moze ostati prazno!";
        isValid = false;
    }
	else if(amount < 1 || amount > 20){
		amountError.innerHTML = "Moze se poruciti izmedju 1 i 20 ptica!";
        isValid = false;
	}

    return isValid;
}

function validateBird(name,amount){
    var birdNameError = document.getElementById("birdNameError");
    birdNameError.innerHTML = "";
	var birdAmountError = document.getElementById("birdAmountError");
    birdAmountError.innerHTML = "";

    var isValid = true;
    if(!name){
        nameError.innerHTML = "Ovo polje ne moze ostati prazno!";
        isValid = false;
    }
	else if(name.length < 2 || name.length > 100){
		nameError.innerHTML = "Naziv moze imati najmanje 2 a najvise 100 karaktera!";
		isValid = false;
	}
	if(!amount){
        amountError.innerHTML = "Ovo polje ne moze ostati prazno!";
        isValid = false;
    }
	else if(amount < 1 || amount > 20){
		amountError.innerHTML = "Moze se poruciti izmedju 1 i 20 ptica!";
        isValid = false;
	}

    return isValid;
}

function createOrder(){
    var orderCustomerName = document.getElementById("orderCustomerName").value;
	var orderCustomerSurname = document.getElementById("orderCustomerSurname").value;
	var orderCustomerAdress = document.getElementById("orderCustomerAdress").value;
    var orderCustomerCity = document.getElementById("orderCustomerCity").value;
	var orderAmount = document.getElementById("orderAmount").value;
    var orderBird = document.getElementById("orderBird").value;
	var httpAction;
	var sendData;
	var url;

	if(validateOrder(orderCustomerName,orderCustomerSurname,orderCustomerAdress,orderCustomerCity,orderAmount)){
		var headers = { 'Content-Type': 'application/json' };
		if (jwt_token) {
			headers.Authorization = 'Bearer ' + jwt_token;		
		}

		if (formAction === "Create") {
			httpAction = "POST";
			url = host + port + ordersEndpoint;
			sendData = {
				"NameOfCustomer": orderCustomerName,
				"SurnameOfCustomer": orderCustomerSurname,
				"AdressOfCustomer": orderCustomerAdress,
				"CustomerCity": orderCustomerCity,
				"Amount": orderAmount,
				"BirdId": orderBird,
				"CustomerIdNumber":Math.floor(Math.random() * 100000)
			};
		}

		console.log("Object to be sent");
		console.log(sendData);

		fetch(url, { method: httpAction, headers: headers, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200 || response.status === 201) {
					console.log("Successful action");
					formAction = "Create";
					document.getElementById("data").innerHTML = "";
					loadOrders();
					loadBirds(setBirds);
					document.getElementById("orderCustomerName").value = "";
					document.getElementById("orderCustomerSurname").value = "";
					document.getElementById("orderCustomerAdress").value = "";
					document.getElementById("orderCustomerCity").value = "";
					document.getElementById("orderAmount").value = "";
				} else {
					console.log("Error occured with code " + response.status);
					alert("Error occured!");
				}
			})
			.catch(error => console.log(error));
	}
	return false;
}

function setBirdGender(){
	var dropdown = document.getElementById("birdGender");
	for (var i = 0; i < 2; i++) {
		var option = document.createElement("option");
		var text = document.createTextNode("");
		if(i == 0){
			option.value = 2;
			text.appendData("Muzjak");
		}
		else{
			option.value = 1;
			text.appendData("Zenka");
		}
		option.appendChild(text);
		dropdown.appendChild(option);
	}
}

function createBird(){
    var birdName = document.getElementById("birdName").value;
	var birdAmount = document.getElementById("birdAmount").value;
    var birdGender = document.getElementById("birdGender").value;
	var httpAction;
	var sendData;
	var url;

	if(validateBird(birdName,birdAmount)){
		var headers = { 'Content-Type': 'application/json' };
		if (jwt_token) {
			headers.Authorization = 'Bearer ' + jwt_token;		
		}

		if (formAction === "Create") {
			httpAction = "POST";
			url = host + port + birdsEndpoint;
			sendData = {
				"Name": birdName,
				"Quantity": birdAmount,
				"Gender.Gender": birdGender
			};
		}

		console.log("Object to be sent");
		console.log(sendData);

		fetch(url, { method: httpAction, headers: headers, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200 || response.status === 201) {
					console.log("Successful action");
					formAction = "Create";
					document.getElementById("data").innerHTML = "";
					loadBirds(setBirds2);
					document.getElementById("birdName").value = "";
				} else {
					console.log("Error occured with code " + response.status);
					alert("Error occured!");
				}
			})
			.catch(error => console.log(error));
	}
	return false;
}

function deleteOglas(){
    var deleteID = this.name;
	var url = host + port + ordersEndpoint + deleteID.toString();
	var headers = { 'Content-Type': 'application/json' };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;		
	}
	fetch(url, { method: "DELETE", headers: headers})
		.then((response) => {
			if (response.status === 204) {
				console.log("Successful action");
                document.getElementById("data").innerHTML = "";
				loadOrders();
			} else {
				console.log("Error occured with code " + response.status);
				alert("Desila se greska!");
			}
		})
		.catch(error => console.log(error));
}

function addBirdQuantity(){
    var birdID = this.name;
	var url = host + port + birdsEndpoint + "add/" + birdID.toString();
	var headers = { 'Content-Type': 'application/json' };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;		
	}
	fetch(url, { method: "POST", headers: headers})
		.then((response) => {
			if (response.status === 200 || response.status === 201) {
				console.log("Successful action");
                document.getElementById("data").innerHTML = "";
				loadBirds(setBirds2);
			} else {
				console.log("Error occured with code " + response.status);
				alert("Desila se greska!");
			}
		})
		.catch(error => console.log(error));
}

function orderSearchOptions(){
	var value = document.getElementById("searchBy").value;

	if(value == 1){
		document.getElementById("searchByName").style.display = "block";
		document.getElementById("searchByCity").style.display = "none";
		document.getElementById("searchByAmount").style.display = "none";
	}
	else if(value == 2){
		document.getElementById("searchByCity").style.display = "block";
		document.getElementById("searchByName").style.display = "none";
		document.getElementById("searchByAmount").style.display = "none";
	}
	else{
		document.getElementById("searchByAmount").style.display = "block";
		document.getElementById("searchByCity").style.display = "none";
		document.getElementById("searchByName").style.display = "none";
	}
}

function fetchSearch(url,sendData){
	var headers = { 'Content-Type': 'application/json' };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;		
	}

	fetch(url, { method: "POST", headers: headers, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200) {
					document.getElementById("data").innerHTML = "";
					response.json().then(setOrders);
				} else {
					console.log("Error occured with code " + response.status);
					alert("Desila se greska prilikom pretrage!");
				}
			})
			.catch(error => console.log(error));
}

function searchByName(){
	var name = document.getElementById("searchName").value;

	var sendData = {
		"NameOfCustomer":name
	};
	var url = host + port + "api/byName";

	console.log("Objekat za slanje");
	console.log(sendData);

	if(!name){
		document.getElementById("data").innerHTML = "";
		loadOrders();
		return false;
	}
	fetchSearch(url,sendData);
	return false;
}

function searchByCity(){
	var city = document.getElementById("searchCity").value;

	var sendData = {
		"CustomerCity":city
	};
	var url = host + port + "api/byCity";
	
	console.log("Objekat za slanje");
	console.log(sendData);
	
	if(!city){
		document.getElementById("data").innerHTML = "";
		loadOrders();
		return false;
	}
	fetchSearch(url,sendData);
	return false;
}	

function searchByAmount(){
	var min = document.getElementById("searchMin").value;
	var max = document.getElementById("searchMax").value;
		
	var sendData = {
		"Min":min,
		"Max":max,
	};
	var url = host + port + "api/byAmount";
	
	console.log("Objekat za slanje");
	console.log(sendData);
	
	if(!min && !max){
		document.getElementById("data").innerHTML = "";
		loadOrders();
		return false;
	}
	fetchSearch(url,sendData);
	return false;
}

function clearForm(){
    document.getElementById("orderCustomerName").value = "";
	document.getElementById("nameError").innerHTML = "";
    document.getElementById("orderCustomerSurname").value = "";
	document.getElementById("surnameError").innerHTML = "";
    document.getElementById("orderCustomerAdress").value = "";
	document.getElementById("adressError").innerHTML = "";
    document.getElementById("orderCustomerCity").value = "";
	document.getElementById("cityError").innerHTML = "";
	document.getElementById("orderAmount").value = "";
	document.getElementById("amountError").innerHTML = "";

    return false;
}

function backButton(bckButton){
	bckButton();
}
function backButton1(){
	document.getElementById("createForm").style.display = "none";
	document.getElementById("index").style.display = "block";
	document.getElementById("data").style.display = "block";
}
function backButton2(){
	document.getElementById("data").innerHTML = "";
	document.getElementById("data").style.display = "block";
	loadBirds(setBirds2);
	document.getElementById("pretragaForm").style.display = "none";
	document.getElementById("createForm").style.display = "none";
	document.getElementById("index").style.display = "block";
	document.getElementById("infoForm").style.display = "block";
	document.getElementById("backButtonForm").style.display = "none";
	document.getElementById("backBtn2").style.display = "inline";
	document.getElementById("btnAfter").style.display = "inline";
	document.getElementById("indexDisplay").style.display = "none";
	if (isAdmin()) {
		document.getElementById("birdLink").style.display = "block";
	}
	document.getElementById("createBirdForm").style.display = "none";
}