

//Navbar active page script
document.addEventListener('DOMContentLoaded', function () {
    var navLinks = document.querySelectorAll('.nav-link');
    for (var i = 0; i < navLinks.length; i++) {
        var link = navLinks[i];
        if (link.href === window.location.href) {
            link.classList.add('active');
        }
    }
});
//Navbar active page script

//PreLoader
document.addEventListener('DOMContentLoaded', function () {
    var loadingScreen = document.querySelector('#overlay');
    setTimeout(function () {
        loadingScreen.style.opacity = '0';
        setTimeout(function () {
            loadingScreen.style.display = 'none';
        }, 1000);
    }, 1000);
});


function replyClick(Id) {

    var divElement = document.getElementById(Id.toString());

    var existingReplyDiv = divElement.querySelector(".replyDiv");
    var replyForm = document.querySelectorAll('.replyForm');
    var form = `
    <div class="replyForm">
        
            <input type="hidden" id="SpamControl">
                <span class="head pt-1">AD-SOYAD</span>
                <input type="text" id="AuthorName"  maxlength="50" placeholder="Ad Soyad" class="inputS mb-1 py-1 my-1">
                <span class="ml-1" style="color:red;font-weight:bold;" id="AuthorNameErrorSpan"></span>
                <br />

                <span class="head" style="display:flex;">EMAIL</span>
                <input id="AuthorEmailAddress" type="email" placeholder="Email" class="inputS mb-1 py-1 my-1">
                <span class="ml-1" style="color:red;font-weight:bold;" id="AuthorEmailAddressErrorSpan"></span>
                <br />

                    <span class="head">YORUM</span>
                    <textarea id="CommentText" placeholder="Yorum" rows="2" class="inputS mb-1 my-1"></textarea>
                    <span class="ml-1" style="color:red;font-weight:bold;" id="CommentTextErrorSpan"></span>
                <br />

                    <div class="row">
                        <input class="replyCancelButton m-auto p-1" type="button" name="" id="" value="IPTAL" onclick="replyCancel()">
                            <button class="rply" onclick="reply(${Id})">
                                <input class="replySendButton m-auto p-1" type="button" name="" id="" value="GONDER">
                            </button>
                    </div>
                
            </div>
            `;


    if (replyForm.length == 0) {
        existingReplyDiv.innerHTML = form;
    }
    else if (replyForm.length == 1) {
        document.querySelector(".replyForm").remove()
        existingReplyDiv.innerHTML = form;
    }

}

function replyCancel() {
    document.querySelector(".replyForm").remove()
}

const reply = (Id) => {

    var replyForm = document.querySelector('.replyForm');

    var AuthorName = document.querySelector("#AuthorName");
    var AuthorEmailAddress = document.querySelector("#AuthorEmailAddress");
    var CommentText = document.querySelector("#CommentText");
    var CommetId = Id;


    var AuthorNameValidation = document.getElementById("AuthorNameErrorSpan");
    var AuthorEmailAddressValidation = document.getElementById("AuthorEmailAddressErrorSpan");
    var CommentTextValidation = document.getElementById("CommentTextErrorSpan");

    if (AuthorName.value.length > 50) {
        toastr.warning("AD Soyad Alaný 50 Karakterden Uzun Olamaz!");
        return;
    }

    if (AuthorName.value.trim() === "") {
        AuthorNameValidation.innerHTML = "Please Enter Your Name";
    } else {
        AuthorNameValidation.innerHTML = "";
    }

    if (AuthorEmailAddress.value.trim() === "") {
        AuthorEmailAddressValidation.innerHTML = "Please Enter Your Email Address";
    } else {
        AuthorEmailAddressValidation.innerHTML = "";
    }

    if (CommentText.value.trim() === "") {
        CommentTextValidation.innerHTML = "Please Enter Your Comment";
    } else {
        CommentTextValidation.innerHTML = "";
    }

    var model = {
        AuthorName: AuthorName.value,
        CommentText: CommentText.value,
        AuthorEmailAddress: AuthorEmailAddress.value,
        CommentId: Id
    };

    console.log(model)

    if (model.AuthorName.value !== "" && model.CommentText.value !== "" && model.AuthorEmailAddress.value !== "" && model.CommentId !== "") {
        fetch('/Home/ReplyCreate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(model)
        })
            .then(data => data.json())
            .then(data => {
                if (data === true) {
                    toastr.info('Your comment is sent. It will be published after review');

                    AuthorName.value = "";
                    AuthorEmailAddress.value = "";
                    CommentText.value = "";

                } else {
                    toastr.warning('Comment could not be sent!');
                }
            });
    } else {
        toastr.error('Error!');
    }
};

function commentCreate() {

    var AuthorName = document.querySelector("#AuthorName");
    var AuthorEmailAddress = document.querySelector("#AuthorEmailAddress");
    var CommentText = document.querySelector("#CommentText");
    var blogId = document.querySelector("#blogId");
    var SpamControl = document.querySelector("#SpamControl");

    console.log(AuthorName)

    if (SpamControl.value.length > 0) {
        toastr.error("Spam Comment!");

        window.location.href = "/Home/Index";
        return;
    }

    var AuthorNameValidation = document.getElementById("AuthorNameErrorSpan");
    var AuthorEmailAddressValidation = document.getElementById("AuthorEmailAddressErrorSpan");
    var CommentTextValidation = document.getElementById("CommentTextErrorSpan");

    if (AuthorName.value.trim() === "") {
        AuthorNameValidation.innerHTML = "Please Enter Your Name";
    } else {
        AuthorNameValidation.innerHTML = "";
    }

    console.log(AuthorName)
    if (AuthorName.value.length > 51) {
        AuthorNameValidation.innerHTML = "Please Enter at least 50 character Name";
    } else {
        AuthorNameValidation.innerHTML = "";
    }

    if (AuthorEmailAddress.value.trim() === "") {
        AuthorEmailAddressValidation.innerHTML = "Please Enter Your Email Address";
    } else {
        AuthorEmailAddressValidation.innerHTML = "";
    }

    if (CommentText.value.trim() === "") {
        CommentTextValidation.innerHTML = "Please Enter Your Comment";
    } else {
        CommentTextValidation.innerHTML = "";
    }

    var model = {
        AuthorName: AuthorName.value,
        AuthorEmailAddress: AuthorEmailAddress.value,
        CommentText: CommentText.value,
        BlogId: blogId.value
    };

    if (model.AuthorName.value != "" && model.CommentText.value != "" && model.AuthorEmailAddress.value != "" && model.BlogId.value != "") {
        fetch('/Home/CommentCreate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(model)
        })
            .then(data => data.json())
            .then(data => {
                if (data === true) {
                    toastr.info('Your comment is sent. It will be published after review');

                    AuthorName.value = "";
                    AuthorEmailAddress.value = "";
                    CommentText.value = "";

                } else {
                    toastr.warning('Comment could not be sent!');
                }
            });
    } else {
        toastr.error('Error!');
        return false;
    }
}

function Signup() {
    console.log('deneme!')
    var username = document.querySelector("#Username");
    var email = document.querySelector("#UserMail");
    var password = document.querySelector("#UserPassword");
    var passwordRepeat = document.querySelector("#UserPasswordRepeat");
    var spamControl = document.querySelector("#SpamControl");

    if (spamControl.value.length > 0) {
        toastr.error("Spam Comment!");

        window.location.href = "/Account/Signup";
        return;
    }

    if (username.value.trim() === "") {
        toastr.warning('Please Entry Your Username')

        return;
    }

    if (email.value.trim() === "") {
        toastr.warning('Please Entry Your Email')

        return;
    }

    if (password.value.trim() === "") {
        toastr.warning('Please Entry Your Password')

        return;
    }
    if (passwordRepeat.value.trim() === "") {
        toastr.warning('Please Entry Your Password Repeat')

        return;
    }

    if (password.value.trim() != passwordRepeat.value.trim()) {
        toastr.warning('The entered password and the re-entered password did not match!')

        return;
    }

    var model = {
        UserName : username.value,
        Email : email.value,
        Password : password.value,
        ConfirmPassword : passwordRepeat.value
    };

    fetch('/Account/Signup', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(model)
    })
        .then(data => data.json())
        .then(data => {
            if (data === true) {

                window.location.href = "/Account/Login";

            } else {
                toastr.error('Error! ' + data);
            }
        });
}

function Login() {

    console.log('deneme!')


    var username = document.querySelector("#Username");
    var password = document.querySelector("#UserPassword");
    var spamControl = document.querySelector("#SpamControl");

    if (spamControl.value.length > 0) {
        toastr.error("Spam Comment!");

        window.location.href = "/Account/Signup";
        return;
    }

    if (username.value.trim() === "") {
        toastr.warning('Please Entry Your Username')

        return;
    }

    if (password.value.trim() === "") {
        toastr.warning('Please Entry Your Password')

        return;
    }

    var model = {
        UserName: username.value,
        Password: password.value
    };

    fetch('/Account/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(model)
    })
        .then(data => data.json())
        .then(data => {
            if (data === true) {

                window.location.href = "/Dashboard/Index";

            } else {
                toastr.error('Error! ' + data);
            }
        });
}
