import "./loginSignup.css";

interface LoginSignUpButtonProps {
  apiEndpoint: string;
  label: string;
}

function LoginSignupButton({ apiEndpoint, label }: LoginSignUpButtonProps) {
  return (
    <button onClick={() => (window.location.href = apiEndpoint)}>
      {label}
    </button>
  );
}

export default function LoginSignup() {
  return (
    <div className="loginSignupWrapper">
      <p>Welcome to Fitness Progress Tracker</p>
      <div className="loginSignupButtonWrapper">
        <p>If you have an account, click here to login</p>
        <LoginSignupButton
          apiEndpoint={"https://localhost:7206/UserAccount/login"}
          label={"Login"}
        />
      </div>
      <div className="loginSignupButtonWrapper">
        <p>If you don't have an account, click here to sign up</p>
        <LoginSignupButton
          apiEndpoint={"https://localhost:7206/UserAccount/SignUp"}
          label={"Sign up"}
        />
      </div>
    </div>
  );
}
