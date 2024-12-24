import React from "react";
import "./LoginPage.css"; // We'll define some basic styling here
import Logo from "../assests/Logo.jpg"

const LoginPage = () => {
  return (
    <div className="login-page">
      
      <div className="login-left">
        {/**  ********************************************************************** */}
        <div className="logo-area">
          <div><img className="logo-img" src={Logo} alt="Fit-On Logo"/></div>
          <div className="logo-text">
            <p>Fit-on</p>
          </div>
            <div className="signup-link">
             Don’t have an account? <a href="#sign-up">Sign Up!</a>
           </div>
        </div>
          {/**  ********************************************************************** */}

        <div className="login-greeting">
            <h1>Welcome Back</h1>
            <p>Login into your account</p>
        </div>
        {/**  ********************************************************************** */}
        
        <div className="social-logins">
            <div className="button-group">
              <button className="google-btn">Google</button>
              <button className="facebook-btn">Facebook</button>
             </div>
            <div className="or-continue">Or continue with</div>
        </div>

        {/**  ********************************************************************** */}
        <div className="login-content">

          

          
          <form className="login-form">
            <div className="form-group">
              <label htmlFor="email">Email</label>
              <input id="email" type="email" placeholder="Enter your email" />
            </div>

            <div className="form-group">
              <label htmlFor="password">Password</label>
              <input
                id="password"
                type="password"
                placeholder="Enter your password"
              />
            </div>

            
            <div className="form-options">
              <label>
                <input type="checkbox" />
                Remember me
              </label>
              <a href="#recover-password" className="recover-link">
                Recover Password
              </a>
            </div>

            
            <button type="submit" className="login-btn">
              Log In
            </button>
          </form>
        </div>
      </div>

      
      <div className="login-right">
        <img
          className="promo-image"
          src="https://via.placeholder.com/600x800"
          alt="Promotional"
        />
        <div className="promo-text">
          <span role="img" aria-label="empower-icon">
            👍
          </span>{" "}
          <strong>Empowering Your Everyday</strong>
          <p>
            We craft innovative solutions to simplify consumer challenges,
            making your life easier and more stylish.
          </p>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
