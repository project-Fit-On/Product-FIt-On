import Link from "next/link";
import Image from "next/image";
import styles from "./Footer.module.css";

export default function Footer() {
  const currentYear = new Date().getFullYear();

  return (
    <footer className={styles.footer}>
      <div className={styles.topSection}>
        <div className={styles.container}>
          <div className={styles.footerGrid}>
            <div className={styles.footerBrand}>
              <div className={styles.footerLogo}>
                <Image
                  src="/img/logo.png"
                  alt="FitOn Logo"
                  width={120}
                  height={120}
                  className={styles.logo}
                />
              </div>
              <p className={styles.tagline}>Try Before You Buy</p>
              <p className={styles.description}>
                FitOn is revolutionizing how you shop for clothes with our
                cutting-edge virtual fitting technology.
              </p>
            </div>

            <div className={styles.footerLinks}>
              <h3>Quick Links</h3>
              <ul>
                <li>
                  <Link href="/">Home</Link>
                </li>
                <li>
                  <Link href="/aboutus">About Us</Link>
                </li>
                <li>
                  <Link href="/catalog">Shop</Link>
                </li>
                <li>
                  <Link href="/services">Services</Link>
                </li>
                <li>
                  <Link href="/contact">Contact</Link>
                </li>
              </ul>
            </div>

            <div className={styles.footerLinks}>
              <h3>Support</h3>
              <ul>
                <li>
                  <Link href="/faq">FAQ</Link>
                </li>
                <li>
                  <Link href="/shipping">Shipping</Link>
                </li>
                <li>
                  <Link href="/returns">Returns</Link>
                </li>
                <li>
                  <Link href="/privacy">Privacy Policy</Link>
                </li>
                <li>
                  <Link href="/terms">Terms of Service</Link>
                </li>
              </ul>
            </div>

            <div className={styles.footerNewsletter}>
              <h3>Stay Updated</h3>
              <p>Subscribe to receive the latest news and promotions.</p>
              <form className={styles.subscribeForm}>
                <input
                  type="email"
                  placeholder="Your email address"
                  className={styles.emailInput}
                  aria-label="Email for newsletter"
                />
                <button type="submit" className={styles.submitBtn}>
                  Subscribe
                </button>
              </form>
            </div>
          </div>
        </div>
      </div>

      <div className={styles.middleSection}>
        <div className={styles.container}>
          <div className={styles.social}>
            <a
              href="https://facebook.com"
              aria-label="Facebook"
              className={styles.socialLink}
            >
              <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                width="24"
                height="24"
              >
                <path fill="none" d="M0 0h24v24H0z" />
                <path d="M14 13.5h2.5l1-4H14v-2c0-1.03 0-2 2-2h1.5V2.14c-.326-.043-1.557-.14-2.857-.14C11.928 2 10 3.657 10 6.7v2.8H7v4h3V22h4v-8.5z" />
              </svg>
            </a>
            <a
              href="https://instagram.com"
              aria-label="Instagram"
              className={styles.socialLink}
            >
              <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                width="24"
                height="24"
              >
                <path fill="none" d="M0 0h24v24H0z" />
                <path d="M12 2c2.717 0 3.056.01 4.122.06 1.065.05 1.79.217 2.428.465.66.254 1.216.598 1.772 1.153a4.908 4.908 0 0 1 1.153 1.772c.247.637.415 1.363.465 2.428.047 1.066.06 1.405.06 4.122 0 2.717-.01 3.056-.06 4.122-.05 1.065-.218 1.79-.465 2.428a4.883 4.883 0 0 1-1.153 1.772 4.915 4.915 0 0 1-1.772 1.153c-.637.247-1.363.415-2.428.465-1.066.047-1.405.06-4.122.06-2.717 0-3.056-.01-4.122-.06-1.065-.05-1.79-.218-2.428-.465a4.89 4.89 0 0 1-1.772-1.153 4.904 4.904 0 0 1-1.153-1.772c-.248-.637-.415-1.363-.465-2.428C2.013 15.056 2 14.717 2 12c0-2.717.01-3.056.06-4.122.05-1.066.217-1.79.465-2.428a4.88 4.88 0 0 1 1.153-1.772A4.897 4.897 0 0 1 5.45 2.525c.638-.248 1.362-.415 2.428-.465C8.944 2.013 9.283 2 12 2zm0 1.802c-2.67 0-2.986.01-4.04.059-.976.045-1.505.207-1.858.344-.466.182-.8.398-1.15.748-.35.35-.566.684-.748 1.15-.137.353-.3.882-.344 1.857-.048 1.055-.058 1.37-.058 4.04 0 2.67.01 2.986.058 4.04.045.976.207 1.504.344 1.857.182.466.398.8.748 1.15.35.35.684.566 1.15.748.353.137.882.3 1.857.344 1.054.048 1.37.058 4.04.058 2.67 0 2.987-.01 4.04-.058.976-.045 1.505-.207 1.858-.344.466-.182.8-.398 1.15-.748.35-.35.566-.684.748-1.15.137-.353.3-.882.344-1.857.048-1.055.058-1.37.058-4.04 0-2.67-.01-2.986-.058-4.04-.045-.976-.207-1.505-.344-1.858a3.097 3.097 0 0 0-.748-1.15 3.098 3.098 0 0 0-1.15-.748c-.353-.137-.882-.3-1.857-.344-1.054-.048-1.37-.058-4.04-.058zm0 3.063a5.135 5.135 0 1 1 0 10.27 5.135 5.135 0 0 1 0-10.27zm0 8.468a3.333 3.333 0 1 0 0-6.666 3.333 3.333 0 0 0 0 6.666zm6.538-8.684a1.2 1.2 0 1 1-2.4 0 1.2 1.2 0 0 1 2.4 0z" />
              </svg>
            </a>
            <a
              href="https://twitter.com"
              aria-label="Twitter"
              className={styles.socialLink}
            >
              <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                width="24"
                height="24"
              >
                <path fill="none" d="M0 0h24v24H0z" />
                <path d="M22.162 5.656a8.384 8.384 0 0 1-2.402.658A4.196 4.196 0 0 0 21.6 4c-.82.488-1.719.83-2.656 1.015a4.182 4.182 0 0 0-7.126 3.814 11.874 11.874 0 0 1-8.62-4.37 4.168 4.168 0 0 0-.566 2.103c0 1.45.738 2.731 1.86 3.481a4.168 4.168 0 0 1-1.894-.523v.052a4.185 4.185 0 0 0 3.355 4.101 4.21 4.21 0 0 1-1.89.072A4.185 4.185 0 0 0 7.97 16.65a8.394 8.394 0 0 1-6.19 1.732 11.83 11.83 0 0 0 6.41 1.88c7.693 0 11.9-6.373 11.9-11.9 0-.18-.005-.362-.013-.54a8.496 8.496 0 0 0 2.087-2.165z" />
              </svg>
            </a>
            <a
              href="https://linkedin.com"
              aria-label="LinkedIn"
              className={styles.socialLink}
            >
              <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                width="24"
                height="24"
              >
                <path fill="none" d="M0 0h24v24H0z" />
                <path d="M6.94 5a2 2 0 1 1-4-.002 2 2 0 0 1 4 .002zM7 8.48H3V21h4V8.48zm6.32 0H9.34V21h3.94v-6.57c0-3.66 4.77-4 4.77 0V21H22v-7.93c0-6.17-7.06-5.94-8.72-2.91l.04-1.68z" />
              </svg>
            </a>
          </div>
        </div>
      </div>

      <div className={styles.bottomSection}>
        <div className={styles.container}>
          <div className={styles.footerBottom}>
            <p className={styles.copyright}>
              © {currentYear} FitOn. All rights reserved.
            </p>
            <div className={styles.paymentMethods}>
              <span>Payment Methods:</span>
              <div className={styles.paymentIcons}>
                <div className={styles.paymentIcon}>
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    width="20"
                    height="20"
                  >
                    <path fill="none" d="M0 0h24v24H0z" />
                    <path d="M22 6h-7.25a.75.75 0 1 0 0 1.5H22v9a1.5 1.5 0 0 1-1.5 1.5h-17A1.5 1.5 0 0 1 2 16.5v-9H3.75a.75.75 0 1 0 0-1.5H2a2 2 0 0 0-2 2v9a3 3 0 0 0 3 3h17a3 3 0 0 0 3-3V8a2 2 0 0 0-1-1.732zM15 3a3 3 0 0 0-3 3h-3a3 3 0 0 0-3-3h9z" />
                  </svg>
                </div>
                <div className={styles.paymentIcon}>
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    width="20"
                    height="20"
                  >
                    <path fill="none" d="M0 0h24v24H0z" />
                    <path d="M1 4h22v2H1V4zm0 14h22v2H1v-2zm18.622-3.086l-.174-.87h-1.949l-.31.863-1.562.003L18.75 5.8h1.44l3.35 9.111h-1.49l-.174.003zM7.872 9.106H3.584v1.142h3.992v1.022H3.584v1.148h4.296v1.107H2.241V8h5.631v1.106zm3.541 4.313c-.697 0-1.355-.355-1.345-1.107.007-.714.456-1.13 1.284-1.342.462-.118 1.347-.196 1.998-.396v.67c0 1.15-.627 2.175-1.937 2.175zm2.824-4.036c0-.511-.26-.937-.825-1.026-.325-.051-1.101-.068-1.845.01-.311.035-.621.074-.936.127l-.126-1.032c.457-.106.943-.157 1.429-.157 1.023 0 1.975.175 2.456.487.713.33 1.073.991 1.073 2.224v3.273h-1.2l-.105-.724c-.32.31-.796.576-1.324.7-.563.13-1.156.122-1.756-.044-1.083-.318-1.669-1.109-1.669-2.202 0-1.076.635-1.94 1.67-2.307.829-.296 1.998-.276 3.138-.062l.02.763zM11.46 7.121l.526 3.193a7.35 7.35 0 0 0 .164 1.483h1.2c-.063-.356-.105-.735-.158-1.161L12.74 7.12h-1.28zm8.753 0l-.484 2.637c-.083.431-.13.815-.164 1.17h1.2c.03-.34.093-.72.164-1.142l.496-2.664h-1.212z" />
                  </svg>
                </div>
                <div className={styles.paymentIcon}>
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    width="20"
                    height="20"
                  >
                    <path fill="none" d="M0 0h24v24H0z" />
                    <path d="M15 17a7.5 7.5 0 1 1 0-15 7.5 7.5 0 0 1 0 15zM2 2h4v20H2V2z" />
                  </svg>
                </div>
                <div className={styles.paymentIcon}>
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    width="20"
                    height="20"
                  >
                    <path fill="none" d="M0 0h24v24H0z" />
                    <path d="M3 3h18a1 1 0 0 1 1 1v16a1 1 0 0 1-1 1H3a1 1 0 0 1-1-1V4a1 1 0 0 1 1-1zm9.5 10a2.5 2.5 0 1 0 0-5 2.5 2.5 0 0 0 0 5z" />
                  </svg>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </footer>
  );
}
