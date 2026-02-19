import axios from "axios";
import { useState } from "react";
export default function AccessTests() {
  const [message, setMessage] = useState<string>("");
  const baseUrl: string = "https://localhost:7206/UserAccount";

  const checkUserRole = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7206/UserAccount/testUserRole",
        { withCredentials: true },
      );
      setMessage(`User role access granted: ${response.data.Username}`);
    } catch (error: any) {
      if (error.response) {
        setMessage(`User role access denied: ${error.response.status}`);
      } else {
        setMessage("Error checking user role");
      }
    }
  };

  const checkSuperUserRole = async () => {
    try {
      const response = await axios.get(baseUrl + "/testSuperUserRole", {
        withCredentials: true,
      });
      setMessage(`SuperUser role access granted: ${response.data.Username}`);
    } catch (error: any) {
      if (error.response) {
        setMessage(`SuperUser role access denied: ${error.response.status}`);
      } else {
        setMessage("Error checking superuser role");
      }
    }
  };

  const checkCookie = async () => {
    try {
      const response = await axios.get(baseUrl + "/checkcookie", {
        withCredentials: true,
      });
      setMessage(
        `Cookie check → Authenticated: ${response.data.authenticated}, Username: ${response.data.username}, Roles: ${response.data.roles?.join(", ")}`,
      );
    } catch (error: any) {
      if (error.response) {
        setMessage(`Cookie check failed: ${error.response.status}`);
      } else {
        setMessage("Error checking cookie");
      }
    }
  };

  return (
    <div className="p-4 space-y-4">
      <div className="flex space-x-4">
        <button onClick={checkUserRole}>Check User Role</button>
        <button onClick={checkSuperUserRole}>Check SuperUser Role</button>
        <button onClick={checkCookie}>Check Cookie</button>
      </div>
      {message && <p className="testText">{message}</p>}
    </div>
  );
}
