Pass-through Authentication
====


`GET or POST /auth/<client_id>(/<redirect_path>)`

Allows pass-through authentication based on an ecrypted email address,
which will be used to log in to an existing account, 
or a new account created based upon the email address given.
If decryption of the email address fails, we will attempt to
redirect to the given path but without logging in.

URL Segments:  
`<client_id>`: The client ID, as issued by Cornerstone  
`<redirect_path>`: (optional) After auth, the client (browser) redirects to this path, local to the domain. 

Parameters:  
`auth`: Email address, encrypted using AES-128-CBC encrypted using a shared key Cornerstone provides.  
`test`: (optional) If included, instead of redirecting, reports success of decryption.

Example (browser-pastable):

    data:text/html,
    <form method="post" action="https://give.cornerstone.cc/auth/Gearbox/transactions">
    <button name="auth" value="J7uFDlbVmMc3I9/Aa6J+LgbxAH72W7KwRRZ0/Wjl5Ebw==">
    Manage Transactions
    </button>
    </form>
