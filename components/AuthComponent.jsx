"use client";

import React, { useState } from "react";
import Image from "next/image";
import styles from "./AuthComponent.module.css";

const AuthComponent = () => {
  const [isSignUp, setIsSignUp] = useState(false);

  const toggleForm = () => {
    setIsSignUp(!isSignUp);
  };

  return (
    <div className={styles.container}>
      <div
        className={`${styles.authContainer} ${isSignUp ? styles.signUp : ""}`}
      >
        {/* Image Section */}
        <div className={styles.imageSection}>
          {/*<Image
            src="/img/logo.png"
            alt="Logo"
            width={150}
            height={50}
            className={styles.logoImage}
            priority
          />*/}
          <div className={styles.backgroundImageWrapper}>
            <Image
              src={isSignUp ? "/img/signupimg.jpg" : "/img/signinimg.jpg"}
              alt="Auth Background"
              fill
              priority
              className={styles.backgroundImage}
            />
          </div>
          <div className={styles.imageOverlay}>
            <div className={styles.overlayContent}>
              <h2>{isSignUp ? "Join Our Community" : "Welcome Back"}</h2>
              <p>
                {isSignUp
                  ? "Start your journey with us and discover amazing possibilities."
                  : "We craft innovative solutions to simplify consumer challenges, making your life easier and more stylish."}
              </p>
            </div>
          </div>
        </div>

        {/* Form Section */}
        <div className={styles.formSection}>
          {/*<div className={styles.logo}>
            <Image
              src="/img/logo1.png"
              alt="Logo"
              width={120}
              height={40}
              priority
            />
          </div> */}

          {isSignUp ? (
            // Sign Up Form
            <div className={styles.form}>
              <h1>Get Started With MAKER</h1>
              <p className={styles.subtitle}>Getting started is easy</p>

              <div className={styles.socialButtons}>
                <button className={styles.socialButton}>
                  <Image
                    src="/img/google.png"
                    alt="Google"
                    width={20}
                    height={20}
                  />
                  Google
                </button>
                <button className={styles.socialButton}>
                  <Image
                    src="/img/facebook.png"
                    alt="Facebook"
                    width={20}
                    height={20}
                  />
                  Facebook
                </button>
              </div>

              <div className={styles.divider}>
                <span>Or continue with</span>
              </div>

              <div className={styles.formFields}>
                <input type="text" placeholder="Full Name" />
                <input type="email" placeholder="Email" />
                <input type="password" placeholder="Password" />
                <input type="password" placeholder="Confirm Password" />
              </div>

              <button className={styles.submitButton}>Create Account</button>

              <p className={styles.switchForm}>
                Already have an account?{" "}
                <button onClick={toggleForm}>Sign in</button>
              </p>
            </div>
          ) : (
            // Sign In Form
            <div className={styles.form}>
              <h1>Welcome Back</h1>
              <p className={styles.subtitle}>Login into your account</p>

              <div className={styles.socialButtons}>
                <button className={styles.socialButton}>
                  <Image
                    src="/img/google.png"
                    alt="Google"
                    width={20}
                    height={20}
                  />
                  Google
                </button>
                <button className={styles.socialButton}>
                  <Image
                    src="/img/facebook.png"
                    alt="Facebook"
                    width={20}
                    height={20}
                  />
                  Facebook
                </button>
              </div>

              <div className={styles.divider}>
                <span>Or continue with</span>
              </div>

              <div className={styles.formFields}>
                <input type="email" placeholder="Email" />
                <input type="password" placeholder="Password" />
              </div>

              <div className={styles.formOptions}>
                <label>
                  <input type="checkbox" />
                  <span>Remember me</span>
                </label>
                <button className={styles.recoverButton}>
                  Recover Password
                </button>
              </div>

              <button className={styles.submitButton}>Log In</button>

              <p className={styles.switchForm}>
                Don't have an account?{" "}
                <button onClick={toggleForm}>Sign up</button>
              </p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default AuthComponent;
