PAX.API
=======

Pure Web API application usign Owin, usefull for making mobile apps with complete separation between client and server side.

Includes posting pictures through the API, Facebook login and token authentification.

![alt tag](https://raw.githubusercontent.com/nightwolfz/PAX.API/master/PAX/Content/preview.png)


# Creating account using POST through facebook
*/api/Account/Login*
> {
> UserName:"{Username to create here}",
> Provider:"Facebook",
> AccessToken:"{access token here}"
> }

# Signing-in using POST through facebook (username kept empty)
*/api/Account/Login*
> {
> UserName:"",
> Provider:"Facebook",
> AccessToken:"{access token here}"
> }
