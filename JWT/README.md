# JWT
### What is the JWT WEB TOKEN?
* Open Standard: Means anywhere, anytime, and anyone can use JWT.
* Secure data transfer between any two bodies, any two users, any two servers.
* It is digitally signed: Information is verified and trusted.
* There is no alteration of data.
* Compact: because JWT can be sent via URL, post request & HTTP header.
* Fast transmission makes JWT more usable.
* Self Contained: because JWT itself holds user information.
* It avoids querying the database more than once after a user is logged in and has been verified.
### JWT is useful for
* Authentication
* Secure data transfer
### JWT Token Structure
* A JWT token contains a Header, a Payload, and a Signature.

### What is the JSON Web Token structure?
In its compact form, JSON Web Tokens consist of three parts separated by dots (.), which are:
* Header
* Payload
* Signature
Therefore, a JWT typically looks like the following.

```
xxxxx.yyyyy.zzzzz
```

Let's break down the different parts.
#### Header
The header typically consists of two parts: the type of the token, which is JWT, and the signing algorithm being used, such as HMAC SHA256 or RSA.

For example:

```
{
    "alg": "HS256",
    "typ": "JWT"
}
```
Then, this JSON is Base64Url encoded to form the first part of the JWT.

### Payload
The second part of the token is the payload, which contains the claims. Claims are statements about an entity (typically, the user) and additional data. There are three types of claims: registered, public, and private claims.
Registered claims: These are a set of predefined claims which are not mandatory but recommended, to provide a set of useful, interoperable claims. Some of them are: iss (issuer), exp (expiration time), sub (subject), aud (audience), and others.
Notice that the claim names are only three characters long as JWT is meant to be compact.
Public claims: These can be defined at will by those using JWTs. But to avoid collisions they should be defined in the IANA JSON Web Token Registry or be defined as a URI that contains a collision resistant namespace.
Private claims: These are the custom claims created to share information between parties that agree on using them and are neither registered or public claims.

An example payload could be:

```sh
{
    "sub": "1234567890",
    "name": "John Doe",
    "admin": true
}
```
The payload is then Base64Url encoded to form the second part of the JSON Web Token.

Do note that for signed tokens this information, though protected against tampering, is readable by anyone. Do not put secret information in the payload or header elements of a JWT unless it is encrypted.

### Signature
To create the signature part you have to take the encoded header, the encoded payload, a secret, the algorithm specified in the header, and sign that.

For example if you want to use the HMAC SHA256 algorithm, the signature will be created in the following way:
sh
```
HMACSHA256(base64UrlEncode(header) + "." + base64UrlEncode(payload), secret)
```
The signature is used to verify the message wasn't changed along the way, and, in the case of tokens signed with a private key, it can also verify that the sender of the JWT is who it says it is.

### Putting all together
The output is three Base64-URL strings separated by dots that can be easily passed in HTML and HTTP environments, while being more compact when compared to XML-based standards such as SAML.
The following shows a JWT that has the previous header and payload encoded, and it is signed with a secret. 


### How do JSON Web Tokens work?
In authentication, when the user successfully logs in using their credentials, a JSON Web Token will be returned. Since tokens are credentials, great care must be taken to prevent security issues. In general, you should not keep tokens longer than required.

You also should not store sensitive session data in browser storage due to lack of security.

Whenever the user wants to access a protected route or resource, the user agent should send the JWT, typically in the Authorization header using the Bearer schema. The content of the header should look like the following:
```
Authorization: Bearer <token>
```
MIT Licensed

**Copyright © 2023-present Vasyl Hulpak**
