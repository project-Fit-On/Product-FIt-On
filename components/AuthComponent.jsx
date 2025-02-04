"use client";

import React, { useState } from "react";
import Image from "next/image";
import styles from "./AuthComponent.module.css";

const AuthComponent = () => {
  const [isSignUp, setIsSignUp] = useState(false);
  const [formData, setFormData] = useState({
    fullName: "",
    email: "",
    password: "",
    confirmPassword: "",
  });
  const [errors, setErrors] = useState({});

  // Toggle between sign-in and sign-up forms
  const toggleForm = () => {
    setIsSignUp(!isSignUp);
    setFormData({ fullName: "", email: "", password: "", confirmPassword: "" });
    setErrors({});
  };

  // Handle input change
  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  // Validation function
  const validateForm = () => {
    let newErrors = {};

    // Validate Full Name (only for Sign-Up)
    if (isSignUp && !formData.fullName.trim()) {
      newErrors.fullName = "Full Name is required";
    }

    // Validate Email
    if (!formData.email.trim()) {
      newErrors.email = "Email is required";
    } else if (!/^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/.test(formData.email)) {
      newErrors.email = "Invalid email format";
    }

    // Validate Password
    if (!formData.password.trim()) {
      newErrors.password = "Password is required";
    } else if (formData.password.length < 6) {
      newErrors.password = "Password must be at least 6 characters";
    }

    // Confirm Password (only for Sign-Up)
    if (isSignUp && formData.password !== formData.confirmPassword) {
      newErrors.confirmPassword = "Passwords do not match";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  // Handle form submission
  const handleSubmit = (e) => {
    e.preventDefault();
    if (validateForm()) {
      alert(isSignUp ? "Sign Up Successful!" : "Login Successful!");
      console.log("Form Data:", formData);
    }
  };

  return (
    <div className={styles.container}>
      <div className={`${styles.authContainer} ${isSignUp ? styles.signUp : ""}`}>
        {/* Image Section */}
        <div className={styles.imageSection}>
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
          <form className={styles.form} onSubmit={handleSubmit}>
            <h1>{isSignUp ? "Get Started With MAKER" : "Welcome Back"}</h1>
            <p className={styles.subtitle}>{isSignUp ? "Getting started is easy" : "Login into your account"}</p>

            {/* Social Login Buttons */}
            <div className={styles.socialButtons}>
              <button type="button" className={styles.socialButton}>
                <Image src="/img/google.png" alt="Google" width={20} height={20} />
                Google
              </button>
              <button type="button" className={styles.socialButton}>
                <Image src="/img/facebook.png" alt="Facebook" width={20} height={20} />
                Facebook
              </button>
            </div>

            <div className={styles.divider}>
              <span>Or continue with</span>
            </div>

            {/* Sign-Up Fields */}
            {isSignUp && (
              <div className={styles.formFields}>
                <input
                  type="text"
                  name="fullName"
                  placeholder="Full Name"
                  value={formData.fullName}
                  onChange={handleChange}
                />
                {errors.fullName && <span className={styles.error}>{errors.fullName}</span>}
              </div>
            )}

            {/* Common Fields (Email & Password) */}
            <div className={styles.formFields}>
              <input
                type="email"
                name="email"
                placeholder="Email"
                value={formData.email}
                onChange={handleChange}
              />
              {errors.email && <span className={styles.error}>{errors.email}</span>}

              <input
                type="password"
                name="password"
                placeholder="Password"
                value={formData.password}
                onChange={handleChange}
              />
                {errors.password && <span className={styles.error}>{errors.password}</span>}
            </div>

            {/* Confirm Password (Only for Sign-Up) */}
            {isSignUp && (
              <div className={styles.formFields}>
                <input
                  type="password"
                  name="confirmPassword"
                  placeholder="Confirm Password"
                  value={formData.confirmPassword}
                  onChange={handleChange}
                />
                {errors.confirmPassword && (
                  <span className={styles.error}>{errors.confirmPassword}</span>
                )}
              </div>
            )}

            {/* Remember Me & Forgot Password (Only for Login) */}
            {!isSignUp && (
              <div className={styles.formOptions}>
                <label>
                  <input type="checkbox" /> <span>Remember me</span>
                </label>
                <button type="button" className={styles.recoverButton}>Recover Password</button>
              </div>
            )}

            {/* Submit Button */}
            <button type="submit" className={styles.submitButton}>
              {isSignUp ? "Create Account" : "Log In"}
            </button>

            {/* Toggle Between Forms */}
            <p className={styles.switchForm}>
              {isSignUp ? "Already have an account?" : "Don't have an account?"}{" "}
              <button type="button" onClick={toggleForm}>{isSignUp ? "Sign in" : "Sign up"}</button>
            </p>
          </form>
        </div>
      </div>
    </div>
  );
};

export default AuthComponent;
