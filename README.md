git clone https://github.com/rpereira14/UserManagementApi.git

cd UserManagementApi/UserManagementApi

docker-compose up

If, you are facing issues loading http://localhost:8080/api/customers 

You choose run the following command after clone

UserManagementApi/UserManagementApi/Run.sh

https://localhost:7201/api/customers

------SAMPLE REQUESTS------
- HttpGET GetAll HttpGet
http://localhost:8080/api/customers

- HttpGET GetCustomer with id = 1
http://localhost:8080/api/customers/1

- HttpPOST AddCustomer
http://localhost:8080/api/customers
request body : 
{"firstName":"Jonh","surname":"Doe","email":"jonh.doe@here.com","password":"2adsafd-c099-4c00-8866-62394b9ase06"}

- HttpPUT UpdateCustomer
http://localhost:8080/api/customers
request body :
{"id":1,"firstName":"Roger","surname":"Smith","email":"roger.pt@here.com","password":"2i123sd-c099-4c00-8866-623ad131e06"}

- HttpDelete DeleteCustomer
http://localhost:8080/api/customers/1
