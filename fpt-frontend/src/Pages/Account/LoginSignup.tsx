interface LoginSignUpButtonProps {
    apiEndpoint: string;
    label: string;
}


function LoginSignupButton({apiEndpoint, label}: LoginSignUpButtonProps) {
    return (
        <button onClick={() => (window.location.href = apiEndpoint)}>{label}</button>
    );
}


export default function LoginSignup() {
    return (
        <>
            <div>
                <LoginSignupButton apiEndpoint={"https://localhost:7206/UserAccount/login"} label={"Login"} />
            </div>
            <div>
                <LoginSignupButton apiEndpoint={"https://localhost:7206/UserAccount/SignUp"} label={"SignUp"} />
            </div>
        </>
    )
}