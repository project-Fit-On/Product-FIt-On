import styles from "./contactus.module.css";
import Header from "../Components/Header";
import Footer from "../Components/Footer";
import {
  FaMapMarkerAlt,
  FaEnvelope,
  FaPhone,
  FaInstagram,
  FaTwitter,
  FaFacebook,
} from "react-icons/fa";

export default function ContactUs() {
  const [formData, setFormData] = useState({
    fullName: "",
    email: "",
    phone: "",
    subject: "General Inquiry",
    message: "",
    file: null,
  });

  const [submitting, setSubmitting] = useState(false);
  const [submitStatus, setSubmitStatus] = useState(null);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleFileChange = (e) => {
    setFormData((prev) => ({ ...prev, file: e.target.files[0] }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setSubmitting(true);

    try {
      // Here you would integrate with your email forwarding service
      // For example using FormSpree, EmailJS, or a custom API endpoint

      // Simulate API call
      await new Promise((resolve) => setTimeout(resolve, 1000));

      console.log("Form submitted:", formData);
      setSubmitStatus({
        type: "success",
        message: "Message sent successfully! We'll get back to you soon.",
      });

      // Reset form after successful submission
      setFormData({
        fullName: "",
        email: "",
        phone: "",
        subject: "General Inquiry",
        message: "",
        file: null,
      });

      // Reset file input
      const fileInput = document.getElementById("file-upload");
      if (fileInput) fileInput.value = "";
    } catch (error) {
      console.error("Error submitting form:", error);
      setSubmitStatus({
        type: "error",
        message: "Something went wrong. Please try again later.",
      });
    } finally {
      setSubmitting(false);
      // Clear status after 5 seconds
      setTimeout(() => setSubmitStatus(null), 5000);
    }
  };

  return (
    <main className={styles.container}>
      <Header />

      <div className={styles.pageHeader}>
        <h1>Contact Us – We're Here to Help!</h1>
      </div>

      <section className={styles.contactSection}>
        <div className={styles.getInTouch}>
          <h2>Get in Touch</h2>
          <p>
            We would love to hear from you! Whether you have questions,
            feedback, or collaboration opportunities, feel free to reach out.
          </p>

          <div className={styles.contactInfo}>
            <div className={styles.infoItem}>
              <FaMapMarkerAlt className={styles.icon} />
              <div>
                <h3>Address</h3>
                <p>123 Fashion Street, Smart City, Fitland</p>
              </div>
            </div>

            <div className={styles.infoItem}>
              <FaEnvelope className={styles.icon} />
              <div>
                <h3>Email</h3>
                <p>support@fiton.com</p>
              </div>
            </div>

            <div className={styles.infoItem}>
              <FaPhone className={styles.icon} />
              <div>
                <h3>Phone</h3>
                <p>+1 234 567 890</p>
              </div>
            </div>
          </div>

          <div className={styles.socialLinks}>
            <h3>Follow Us</h3>
            <div className={styles.socialIcons}>
              <a
                href="https://instagram.com"
                target="_blank"
                rel="noopener noreferrer"
                aria-label="Instagram"
              >
                <FaInstagram />
              </a>
              <a
                href="https://twitter.com"
                target="_blank"
                rel="noopener noreferrer"
                aria-label="Twitter"
              >
                <FaTwitter />
              </a>
              <a
                href="https://facebook.com"
                target="_blank"
                rel="noopener noreferrer"
                aria-label="Facebook"
              >
                <FaFacebook />
              </a>
            </div>
          </div>
        </div>

        <div className={styles.contactForm}>
          <div className={styles.formWrapper}>
            <h2>Send Us a Message</h2>

            {submitStatus && (
              <div
                className={`${styles.statusMessage} ${
                  styles[submitStatus.type]
                }`}
              >
                {submitStatus.message}
              </div>
            )}

            <form onSubmit={handleSubmit}>
              <div className={styles.formGroup}>
                <label htmlFor="fullName">
                  Full Name <span className={styles.required}>*</span>
                </label>
                <input
                  type="text"
                  id="fullName"
                  name="fullName"
                  value={formData.fullName}
                  onChange={handleChange}
                  required
                  placeholder="Your full name"
                />
              </div>

              <div className={styles.formRow}>
                <div className={styles.formGroup}>
                  <label htmlFor="email">
                    Email Address <span className={styles.required}>*</span>
                  </label>
                  <input
                    type="email"
                    id="email"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                    required
                    placeholder="your.email@example.com"
                  />
                </div>

                <div className={styles.formGroup}>
                  <label htmlFor="phone">
                    Phone Number{" "}
                    <span className={styles.optional}>(Optional)</span>
                  </label>
                  <input
                    type="tel"
                    id="phone"
                    name="phone"
                    value={formData.phone}
                    onChange={handleChange}
                    placeholder="Your phone number"
                  />
                </div>
              </div>

              <div className={styles.formGroup}>
                <label htmlFor="subject">
                  Subject <span className={styles.required}>*</span>
                </label>
                <select
                  id="subject"
                  name="subject"
                  value={formData.subject}
                  onChange={handleChange}
                  required
                >
                  <option value="General Inquiry">General Inquiry</option>
                  <option value="Technical Support">Technical Support</option>
                  <option value="Retail Partnership">Retail Partnership</option>
                  <option value="Feedback">Feedback</option>
                </select>
              </div>

              <div className={styles.formGroup}>
                <label htmlFor="message">
                  Message <span className={styles.required}>*</span>
                </label>
                <textarea
                  id="message"
                  name="message"
                  value={formData.message}
                  onChange={handleChange}
                  required
                  placeholder="Your message here..."
                  rows="5"
                ></textarea>
              </div>

              <div className={styles.formGroup}>
                <label htmlFor="file-upload">
                  Attach File{" "}
                  <span className={styles.optional}>(Optional)</span>
                </label>
                <div className={styles.fileUpload}>
                  <input
                    type="file"
                    id="file-upload"
                    onChange={handleFileChange}
                  />
                </div>
              </div>

              <button
                type="submit"
                className={styles.submitButton}
                disabled={submitting}
              >
                {submitting ? "Sending..." : "Send Message"}
              </button>
            </form>
          </div>
        </div>
      </section>

      <section className={styles.mapSection}>
        <iframe
          src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d12345.67890!2d-73.9876!3d40.7488!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0x0!2zM40zMCcwMC4wIk4gNzPCsDA1JzAwLjAiVw!5e0!3m2!1sen!2sus!4v1234567890"
          width="100%"
          height="450"
          style={{ border: 0 }}
          allowFullScreen=""
          loading="lazy"
          referrerPolicy="no-referrer-when-downgrade"
          title="Office Location Map"
        ></iframe>
      </section>

      <Footer />
    </main>
  );
}
