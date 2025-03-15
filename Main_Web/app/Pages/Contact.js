"use client";

import { useState } from "react";
import Image from "next/image";
import styles from "./Contact.module.css";

export default function Contact() {
  const [formData, setFormData] = useState({
    fullName: "",
    email: "",
    phone: "",
    subject: "",
    message: "",
  });

  const [formStatus, setFormStatus] = useState({
    submitting: false,
    success: false,
    error: false,
  });

  const [focused, setFocused] = useState({
    fullName: false,
    email: false,
    phone: false,
    subject: false,
    message: false,
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleFocus = (field) => {
    setFocused((prev) => ({
      ...prev,
      [field]: true,
    }));
  };

  const handleBlur = (field) => {
    // Only set to false if field is empty
    if (!formData[field]) {
      setFocused((prev) => ({
        ...prev,
        [field]: false,
      }));
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    // Set submitting state
    setFormStatus({
      submitting: true,
      success: false,
      error: false,
    });

    // Simulate form submission
    try {
      await new Promise((resolve) => setTimeout(resolve, 1500));

      // Reset form on success
      setFormData({
        fullName: "",
        email: "",
        phone: "",
        subject: "",
        message: "",
      });

      // Reset focused states
      Object.keys(focused).forEach((key) => {
        focused[key] = false;
      });

      // Set success state
      setFormStatus({
        submitting: false,
        success: true,
        error: false,
      });

      // Clear success message after 5 seconds
      setTimeout(() => {
        setFormStatus((prev) => ({
          ...prev,
          success: false,
        }));
      }, 5000);
    } catch (error) {
      setFormStatus({
        submitting: false,
        success: false,
        error: true,
      });
    }
  };

  return (
    <section id="contact" className={styles.contactSection}>
      <div className={styles.container}>
        <div className={styles.heading}>
          <span className={styles.headingAccent}>Get in Touch</span>
          <h2 className={styles.title}>Let's Connect</h2>
          <p className={styles.subtitle}>
            Have questions about FitOn? We're here to help with your fashion
            journey.
          </p>
        </div>

        <div className={styles.content}>
          <div className={styles.imageColumn}>
            <div className={styles.imageWrapper}>
              <Image
                src="/img/banner.gif"
                alt="Fashion consultant"
                fill
                style={{ objectFit: "cover" }}
                priority
                className={styles.contactImage}
              />
              <div className={styles.imageBadge}>
                <span>Your style journey starts here</span>
              </div>
            </div>
          </div>

          <div className={styles.formColumn}>
            <div className={styles.formContainer}>
              <h3 className={styles.formTitle}>Send us a message</h3>

              {formStatus.success && (
                <div className={styles.successMessage}>
                  <svg viewBox="0 0 24 24" className={styles.successIcon}>
                    <path d="M9 16.17L4.83 12l-1.42 1.41L9 19 21 7l-1.41-1.41z"></path>
                  </svg>
                  <p>Thank you! Your message has been sent successfully.</p>
                </div>
              )}

              {formStatus.error && (
                <div className={styles.errorMessage}>
                  <svg viewBox="0 0 24 24" className={styles.errorIcon}>
                    <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z"></path>
                  </svg>
                  <p>Oops! Something went wrong. Please try again.</p>
                </div>
              )}

              <form className={styles.form} onSubmit={handleSubmit}>
                <div className={styles.inputGroup}>
                  <label htmlFor="fullName" className={styles.label}>
                    Full Name <span className={styles.required}>*</span>
                  </label>
                  <div
                    className={`${styles.inputWrapper} ${
                      focused.fullName ? styles.focused : ""
                    }`}
                  >
                    <input
                      id="fullName"
                      name="fullName"
                      type="text"
                      value={formData.fullName}
                      onChange={handleChange}
                      onFocus={() => handleFocus("fullName")}
                      onBlur={() => handleBlur("fullName")}
                      required
                      className={styles.input}
                      placeholder="Your full name"
                    />
                  </div>
                </div>

                <div className={styles.inputGroup}>
                  <label htmlFor="email" className={styles.label}>
                    Email Address <span className={styles.required}>*</span>
                  </label>
                  <div
                    className={`${styles.inputWrapper} ${
                      focused.email ? styles.focused : ""
                    }`}
                  >
                    <input
                      id="email"
                      name="email"
                      type="email"
                      value={formData.email}
                      onChange={handleChange}
                      onFocus={() => handleFocus("email")}
                      onBlur={() => handleBlur("email")}
                      required
                      className={styles.input}
                      placeholder="Your email address"
                    />
                  </div>
                </div>

                <div className={styles.inputGroup}>
                  <label htmlFor="phone" className={styles.label}>
                    Phone Number{" "}
                    <span className={styles.optional}>(optional)</span>
                  </label>
                  <div
                    className={`${styles.inputWrapper} ${
                      focused.phone ? styles.focused : ""
                    }`}
                  >
                    <input
                      id="phone"
                      name="phone"
                      type="tel"
                      value={formData.phone}
                      onChange={handleChange}
                      onFocus={() => handleFocus("phone")}
                      onBlur={() => handleBlur("phone")}
                      className={styles.input}
                      placeholder="Your phone number"
                    />
                  </div>
                </div>

                <div className={styles.inputGroup}>
                  <label htmlFor="subject" className={styles.label}>
                    Subject <span className={styles.required}>*</span>
                  </label>
                  <div
                    className={`${styles.inputWrapper} ${
                      focused.subject ? styles.focused : ""
                    }`}
                  >
                    <input
                      id="subject"
                      name="subject"
                      type="text"
                      value={formData.subject}
                      onChange={handleChange}
                      onFocus={() => handleFocus("subject")}
                      onBlur={() => handleBlur("subject")}
                      required
                      className={styles.input}
                      placeholder="What is your message about?"
                    />
                  </div>
                </div>

                <div className={styles.inputGroup}>
                  <label htmlFor="message" className={styles.label}>
                    Message <span className={styles.required}>*</span>
                  </label>
                  <div
                    className={`${styles.inputWrapper} ${
                      focused.message ? styles.focused : ""
                    }`}
                  >
                    <textarea
                      id="message"
                      name="message"
                      value={formData.message}
                      onChange={handleChange}
                      onFocus={() => handleFocus("message")}
                      onBlur={() => handleBlur("message")}
                      required
                      className={styles.textarea}
                      placeholder="Tell us how we can help you"
                      rows={4}
                    ></textarea>
                  </div>
                </div>

                <button
                  type="submit"
                  className={styles.submitBtn}
                  disabled={formStatus.submitting}
                >
                  {formStatus.submitting ? "Sending..." : "Send Message"}
                  {formStatus.submitting && (
                    <span className={styles.loadingSpinner}></span>
                  )}
                </button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}
