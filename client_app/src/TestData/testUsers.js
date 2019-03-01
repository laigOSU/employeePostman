const USERS_DB = [
    {
        "first_name": "Bruce",
        "last_name": "Wayne",
        "email": "bat@gmail.com",
        "role": "Admin",
        "create_on": "10-05-2018",
        "signature": "img",
        "password": "joker"
    },
    {
        "first_name": "Wonder",
        "last_name": "Woman",
        "email": "fly@gmail.com",
        "role": "User",
        "create_on": "10-05-2018",
        "signature": "img",
        "password": "amazonians"
    }
];

export class ServerSide {
    constructor() {
        this.users = USERS_DB;
    }

    login(data) {
        let email_found = false;
        let password_match = false; 
        let user = {};

        for (var x in this.users) {
            if (data.email === this.users[x].email) {
                user = this.users[x];
                email_found = true;
            }
        }

        if (!email_found)
            return new Error(400);
        else {
            if (data.password === user.password)
                password_match = true;
        }

        if (!password_match)
            return new Error(400);
        else {
            const token = "abcdefg";
            const { password, ...userWithoutPassword } = user;
            return {
                data: {
                    token: token,
                    user: userWithoutPassword
                }
            }
        }

        console.log("email Found: ", email_found);
    }
}