"use client";

import React, { useState, useEffect } from "react";
import Image from "next/image";
import styles from "./login.module.css";

const AuthComponent = () => {
  const [isSignUp, setIsSignUp] = useState(false);

  useEffect(() => {
    console.log("AuthComponent mounted");
    // This will help verify if the component is actually loading
  }, []);

  const toggleForm = () => {
    setIsSignUp(!isSignUp);
  };

  return React.createElement(
    "div",
    { className: styles.container },
    React.createElement(
      "div",
      {
        className: `${styles.authContainer} ${isSignUp ? styles.signUp : ""}`,
      },
      /* Image Section */
      React.createElement(
        "div",
        { className: styles.imageSection },
        /* Commented Image Logo section
        React.createElement(
          Image,
          {
            src: "/img/logo.png",
            alt: "Logo",
            width: 150,
            height: 50,
            className: styles.logoImage,
            priority: true
          }
        ),
        */
        React.createElement(
          "div",
          { className: styles.backgroundImageWrapper },
          React.createElement(Image, {
            src: isSignUp ? "/img/signupimg.jpg" : "/img/signinimg.jpg",
            alt: "Auth Background",
            fill: true,
            priority: true,
            className: styles.backgroundImage,
          })
        ),
        React.createElement(
          "div",
          { className: styles.imageOverlay },
          React.createElement(
            "div",
            { className: styles.overlayContent },
            React.createElement(
              "h2",
              null,
              isSignUp ? "Join Our Community" : "Welcome Back"
            ),
            React.createElement(
              "p",
              null,
              isSignUp
                ? "Start your journey with us and discover amazing possibilities."
                : "We craft innovative solutions to simplify consumer challenges, making your life easier and more stylish."
            )
          )
        )
      ),

      /* Form Section */
      React.createElement(
        "div",
        { className: styles.formSection },
        /* Commented Logo section
        React.createElement(
          "div",
          { className: styles.logo },
          React.createElement(
            Image,
            {
              src: "/img/logo1.png",
              alt: "Logo",
              width: 120,
              height: 40,
              priority: true
            }
          )
        ),
        */

        isSignUp
          ? /* Sign Up Form */
            React.createElement(
              "div",
              { className: styles.form },
              React.createElement("h1", null, "Get Started With MAKER"),
              React.createElement(
                "p",
                { className: styles.subtitle },
                "Getting started is easy"
              ),
              React.createElement(
                "div",
                { className: styles.socialButtons },
                React.createElement(
                  "button",
                  { className: styles.socialButton },
                  React.createElement(Image, {
                    src: "/img/google.png",
                    alt: "Google",
                    width: 20,
                    height: 20,
                  }),
                  "Google"
                ),
                React.createElement(
                  "button",
                  { className: styles.socialButton },
                  React.createElement(Image, {
                    src: "/img/facebook.png",
                    alt: "Facebook",
                    width: 20,
                    height: 20,
                  }),
                  "Facebook"
                )
              ),
              React.createElement(
                "div",
                { className: styles.divider },
                React.createElement("span", null, "Or continue with")
              ),
              React.createElement(
                "div",
                { className: styles.formFields },
                React.createElement("input", {
                  type: "text",
                  placeholder: "Full Name",
                }),
                React.createElement("input", {
                  type: "email",
                  placeholder: "Email",
                }),
                React.createElement("input", {
                  type: "password",
                  placeholder: "Password",
                }),
                React.createElement("input", {
                  type: "password",
                  placeholder: "Confirm Password",
                })
              ),
              React.createElement(
                "button",
                { className: styles.submitButton },
                "Create Account"
              ),
              React.createElement(
                "p",
                { className: styles.switchForm },
                "Already have an account? ",
                React.createElement(
                  "button",
                  { onClick: toggleForm },
                  "Sign in"
                )
              )
            )
          : /* Sign In Form */
            React.createElement(
              "div",
              { className: styles.form },
              React.createElement("h1", null, "Welcome Back"),
              React.createElement(
                "p",
                { className: styles.subtitle },
                "Login into your account"
              ),
              React.createElement(
                "div",
                { className: styles.socialButtons },
                React.createElement(
                  "button",
                  { className: styles.socialButton },
                  React.createElement(Image, {
                    src: "/img/google.png",
                    alt: "Google",
                    width: 20,
                    height: 20,
                  }),
                  "Google"
                ),
                React.createElement(
                  "button",
                  { className: styles.socialButton },
                  React.createElement(Image, {
                    src: "/img/facebook.png",
                    alt: "Facebook",
                    width: 20,
                    height: 20,
                  }),
                  "Facebook"
                )
              ),
              React.createElement(
                "div",
                { className: styles.divider },
                React.createElement("span", null, "Or continue with")
              ),
              React.createElement(
                "div",
                { className: styles.formFields },
                React.createElement("input", {
                  type: "email",
                  placeholder: "Email",
                }),
                React.createElement("input", {
                  type: "password",
                  placeholder: "Password",
                })
              ),
              React.createElement(
                "div",
                { className: styles.formOptions },
                React.createElement(
                  "label",
                  null,
                  React.createElement("input", { type: "checkbox" }),
                  React.createElement("span", null, "Remember me")
                ),
                React.createElement(
                  "button",
                  { className: styles.recoverButton },
                  "Recover Password"
                )
              ),
              React.createElement(
                "button",
                { className: styles.submitButton },
                "Log In"
              ),
              React.createElement(
                "p",
                { className: styles.switchForm },
                "Don't have an account? ",
                React.createElement(
                  "button",
                  { onClick: toggleForm },
                  "Sign up"
                )
              )
            )
      )
    )
  );
};

export default AuthComponent;
