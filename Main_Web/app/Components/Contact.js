"use client";
import { useState } from "react";
import styles from "./Contact.module.css";

const Contact = () => {
  const [formData, setFormData] = useState({
    name: "",
    email: "",
    message: "",
  });

  const [isSubmitted, setIsSubmitted] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // Here you would normally handle the form submission to your backend
    console.log("Form submitted:", formData);

    // Simulate a successful submission
    setIsSubmitted(true);
    setFormData({ name: "", email: "", message: "" });

    // Reset the success message after 5 seconds
    setTimeout(() => {
      setIsSubmitted(false);
    }, 5000);
  };

  return (
    <section id="contact" className={styles.contactSection}>
      <div className={styles.container}>
        <div className={styles.contactContent}>
          <div className={styles.contactInfo}>
            <h2>Get In Touch</h2>
            <p>
              Have questions about FitOn? We're here to help! Reach out to our
              team using any of the contact methods below or send us a message
              using the form.
            </p>

            <div className={styles.contactMethods}>
              <div className={styles.contactMethod}>
                <div className={styles.methodIcon}>📍</div>
                <div>
                  <h3>Visit Us</h3>
                  <p>
                    123 Fashion Avenue, Design District
                    <br />
                    San Francisco, CA 94105
                  </p>
                </div>
              </div>

              <div className={styles.contactMethod}>
                <div className={styles.methodIcon}>📞</div>
                <div>
                  <h3>Call Us</h3>
                  <p>+1 (800) 123-4567</p>
                </div>
              </div>

              <div className={styles.contactMethod}>
                <div className={styles.methodIcon}>✉️</div>
                <div>
                  <h3>Email Us</h3>
                  <p>support@fiton-app.com</p>
                </div>
              </div>
            </div>
          </div>

          <div className={styles.contactForm}>
            <h2>Send Us a Message</h2>

            {isSubmitted && (
              <div className={styles.successMessage}>
                Thank you for your message! We'll get back to you soon.
              </div>
            )}

            <form onSubmit={handleSubmit}>
              <div className={styles.formGroup}>
                <label htmlFor="name">Your Name</label>
                <input
                  type="text"
                  id="name"
                  name="name"
                  value={formData.name}
                  onChange={handleChange}
                  required
                />
              </div>

              <div className={styles.formGroup}>
                <label htmlFor="email">Your Email</label>
                <input
                  type="email"
                  id="email"
                  name="email"
                  value={formData.email}
                  onChange={handleChange}
                  required
                />
              </div>

              <div className={styles.formGroup}>
                <label htmlFor="message">Your Message</label>
                <textarea
                  id="message"
                  name="message"
                  rows="5"
                  value={formData.message}
                  onChange={handleChange}
                  required
                ></textarea>
              </div>

              <button type="submit" className={styles.submitButton}>
                Send Message
              </button>
            </form>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Contact;
