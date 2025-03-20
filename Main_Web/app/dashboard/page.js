import styles from "./dashboard.module.css";
import Link from "next/link";
import Image from "next/image";
import {
  FaHome,
  FaTshirt,
  FaCube,
  FaLightbulb,
  FaShoppingBag,
  FaCog,
  FaBell,
} from "react-icons/fa";
import Header from "../Components/Header";
import Footer from "../Components/Footer";

export default function Dashboard() {
  // Mock user data
  const userData = {
    name: "Emma",
    avatar: "/images/avatar.png",
    recentItems: [
      { id: 1, name: "Blue Dress", image: "/images/placeholder1.jpg" },
      { id: 2, name: "Black Jacket", image: "/images/placeholder2.jpg" },
      { id: 3, name: "White Blouse", image: "/images/placeholder3.jpg" },
    ],
    recommendations: [
      {
        id: 1,
        name: "Floral Skirt",
        image: "/images/placeholder4.jpg",
        match: "92%",
      },
      {
        id: 2,
        name: "Denim Jacket",
        image: "/images/placeholder5.jpg",
        match: "87%",
      },
    ],
    shoppingItems: [
      { id: 1, name: "Summer Collection", count: 12 },
      { id: 2, name: "Saved Items", count: 8 },
      { id: 3, name: "Recently Viewed", count: 24 },
    ],
  };

  return (
    <>
      <Header />
      <main className={styles.dashboardContainer}>
        <div className={styles.topBar}>
          <div className={styles.logoSmall}>
            <h3>Dashboard</h3>
          </div>
          <div className={styles.notificationProfile}>
            <div className={styles.notificationIcon}>
              <FaBell />
              <span className={styles.notificationBadge}>3</span>
            </div>
            <div className={styles.profileInfo}>
              <div className={styles.profileName}>{userData.name}</div>
              <div className={styles.profileAvatar}>
                <Image
                  src={userData.avatar}
                  alt="User avatar"
                  width={40}
                  height={40}
                />
              </div>
            </div>
          </div>
        </div>

        <div className={styles.dashboardLayout}>
          <aside className={styles.sidebar}>
            <div className={styles.logoContainer}>
              <h2 className={styles.logo}>Fit-On</h2>
            </div>
            <nav className={styles.sidebarNav}>
              <Link
                href="/dashboard"
                className={`${styles.navItem} ${styles.active}`}
              >
                <FaHome className={styles.navIcon} />
                <span>Dashboard</span>
              </Link>
              <Link href="/digital-closet" className={styles.navItem}>
                <FaTshirt className={styles.navIcon} />
                <span>Digital Closet</span>
              </Link>
              <Link href="/3d-viewer" className={styles.navItem}>
                <FaCube className={styles.navIcon} />
                <span>3D Model Viewer</span>
              </Link>
              <Link href="/recommendations" className={styles.navItem}>
                <FaLightbulb className={styles.navIcon} />
                <span>Recommendations</span>
              </Link>
              <Link href="/shopping" className={styles.navItem}>
                <FaShoppingBag className={styles.navIcon} />
                <span>Shopping</span>
              </Link>
              <Link href="/settings" className={styles.navItem}>
                <FaCog className={styles.navIcon} />
                <span>Settings</span>
              </Link>
            </nav>
          </aside>

          <section className={styles.mainContent}>
            <div className={styles.welcomeSection}>
              <h1 className={styles.welcomeText}>
                Welcome Back, {userData.name}
              </h1>
              <p className={styles.welcomeSubtext}>
                Here's what's new in your fashion world today
              </p>
            </div>

            <div className={styles.dashboardGrid}>
              <div className={`${styles.dashboardCard} ${styles.closetCard}`}>
                <div className={styles.cardHeader}>
                  <h2 className={styles.cardTitle}>Digital Closet Preview</h2>
                  <Link href="/digital-closet" className={styles.viewAllLink}>
                    View All
                  </Link>
                </div>
                <div className={styles.closetItems}>
                  {userData.recentItems.map((item) => (
                    <div key={item.id} className={styles.closetItem}>
                      <div className={styles.itemImageContainer}>
                        <Image
                          src={item.image}
                          alt={item.name}
                          fill
                          className={styles.itemImage}
                        />
                      </div>
                      <p className={styles.itemName}>{item.name}</p>
                    </div>
                  ))}
                </div>
              </div>

              <div
                className={`${styles.dashboardCard} ${styles.recommendationsCard}`}
              >
                <div className={styles.cardHeader}>
                  <h2 className={styles.cardTitle}>Today's Recommendations</h2>
                  <Link href="/recommendations" className={styles.viewAllLink}>
                    View All
                  </Link>
                </div>
                <div className={styles.recommendationItems}>
                  {userData.recommendations.map((item) => (
                    <div key={item.id} className={styles.recommendationItem}>
                      <div className={styles.itemImageContainer}>
                        <Image
                          src={item.image}
                          alt={item.name}
                          fill
                          className={styles.itemImage}
                        />
                        <div className={styles.matchBadge}>{item.match}</div>
                      </div>
                      <p className={styles.itemName}>{item.name}</p>
                    </div>
                  ))}
                </div>
              </div>

              <div className={`${styles.dashboardCard} ${styles.shoppingCard}`}>
                <div className={styles.cardHeader}>
                  <h2 className={styles.cardTitle}>Shopping Overview</h2>
                  <Link href="/shopping" className={styles.viewAllLink}>
                    View All
                  </Link>
                </div>
                <div className={styles.shoppingStats}>
                  {userData.shoppingItems.map((item) => (
                    <div key={item.id} className={styles.statItem}>
                      <div className={styles.statCount}>{item.count}</div>
                      <p className={styles.statName}>{item.name}</p>
                    </div>
                  ))}
                </div>
              </div>

              <div className={`${styles.dashboardCard} ${styles.activityCard}`}>
                <div className={styles.cardHeader}>
                  <h2 className={styles.cardTitle}>Recent Activity</h2>
                </div>
                <div className={styles.activityFeed}>
                  <div className={styles.activityItem}>
                    <div className={styles.activityIcon}>
                      <FaTshirt />
                    </div>
                    <div className={styles.activityContent}>
                      <p className={styles.activityText}>
                        You added 3 new items to your closet
                      </p>
                      <p className={styles.activityTime}>2 hours ago</p>
                    </div>
                  </div>
                  <div className={styles.activityItem}>
                    <div className={styles.activityIcon}>
                      <FaLightbulb />
                    </div>
                    <div className={styles.activityContent}>
                      <p className={styles.activityText}>
                        New outfit recommendations available
                      </p>
                      <p className={styles.activityTime}>Yesterday</p>
                    </div>
                  </div>
                  <div className={styles.activityItem}>
                    <div className={styles.activityIcon}>
                      <FaShoppingBag />
                    </div>
                    <div className={styles.activityContent}>
                      <p className={styles.activityText}>
                        You saved 4 items to your wishlist
                      </p>
                      <p className={styles.activityTime}>3 days ago</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </section>
        </div>
      </main>
      <Footer />
    </>
  );
}
